using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;


namespace IBL
{
    public partial class BL : IblDrone
    {
        public void AddDrone(Drone droneBl, int stationId)
        {
            if (ExistsIDTaxCheck(dal.GetStations(), droneBl.Id))
                throw new ThereIsAnObjectWithTheSameKeyInTheList();
            if (!ExistsIDTaxCheck(dal.GetCustomers(), stationId))
                throw new KeyNotFoundException("sender not exist");
            dal.AddDrone(droneBl.Id,droneBl.Model,(IDAL.DO.WeightCategories)droneBl.WeightCategory);
            IDAL.DO.Station station = dal.GetStation(stationId);
            DroneToList droneToList = new ()
            {
                Id = droneBl.Id,
                DroneModel = droneBl.Model,
                Weight = droneBl.WeightCategory,
                BatteryStatus = rand.NextDouble() + rand.Next(20, 40),
                DroneStatus = DroneStatuses.MAINTENANCE,
                CurrentLocation = new Location() { Latitude = station.Latitude, Longitude = station.Longitude }
            };
            drones.Add(droneToList);
        }
        public BO.Drone GetDrone(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            return MapDrone(dal.GetDrone(id));
        }
        public void ParcelCollectionByDrone(int DroneId)
        {
            DroneToList droneToList = drones.Find(item => item.Id == DroneId);
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
            if (!parcel.PickedUp.Equals(default))
                throw new ArgumentNullException("The package has already been collected");
            drones.Remove(droneToList); 
            IDAL.DO.Customer customer = dal.GetCustomer(parcel.SenderId);
            Location senderLocation = new () { Longitude = customer.Longitude, Latitude = customer.Latitude };
            droneToList.BatteryStatus -= Distance(droneToList.CurrentLocation, senderLocation)*dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            droneToList.CurrentLocation = senderLocation;
            drones.Add(droneToList);
            ParcelcollectionDrone(parcel.Id);
        }
        public void ReleaseDroneFromCharging(int id, float timeOfCharg)
        {
            DroneToList droneToList = drones.Find(item => item.Id == id);
            if (droneToList.DroneStatus != DroneStatuses.MAINTENANCE)
                throw new InvalidEnumArgumentException();
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatuses.AVAILABLE;
            droneToList.BatteryStatus += timeOfCharg / 60 * dal.GetElectricityUse()[4];
            dal.RemoveDroneCharge(id);
        }

        public void SendDroneForCharg(int id)
        {
            DroneToList droneToList = drones.Find(item => item.Id == id);
            if (droneToList.DroneStatus != DroneStatuses.AVAILABLE)
                throw new InvalidEnumArgumentException();
            double minDistance;
            IDAL.DO.Station station = ClosetStationPossible(dal.GetStations(), droneToList,out minDistance);
            if (station.Equals(default))
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException();
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatuses.MAINTENANCE;
            droneToList.BatteryStatus -= minDistance * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            droneToList.CurrentLocation = new Location() { Longitude = station.Longitude, Latitude = station.Latitude };;
            //הורדת מספר עמדות טעינה בתחנה
            dal.AddDRoneCharge(id, station.Id);
        }
        public void UpdateDrone(int id, string name)
        {
            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Drone droneDl = dal.GetDrone(id);
            if (name.Equals(default))
                throw new ArgumentNullException("For updating the name must be initialized ");
            dal.RemoveDrone(droneDl);
            dal.AddDrone(id, name, droneDl.MaxWeight);
            DroneToList droneToList = drones.Find(item => item.Id == id);
            drones.Remove(droneToList);
            droneToList.DroneModel = name;
            drones.Add(droneToList);
        }
        public IEnumerable<DroneToList> GetDrones() => drones;
        private List<DroneInCharging> CreatListDroneInCharging(int id)
        {
            List<int> list = dal.GetDronechargingInStation(id);
            List<DroneInCharging> droneInChargings = new();
            DroneToList droneToList;
            foreach (var idDrone in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.Id == idDrone));
                if (!droneToList.Equals(default))
                {
                    droneInChargings.Add(new DroneInCharging() { Id = idDrone, ChargingMode = droneToList.BatteryStatus });
                }
            }
            return droneInChargings;
        }
        private BO.Drone MapDrone(IDAL.DO.Drone drone)
        {
            DroneToList droneToList = drones.Find(item => item.Id == drone.Id);
            return new Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                WeightCategory = (WeightCategories)drone.MaxWeight,
                DroneStatus = droneToList.DroneStatus,
                BattaryMode = droneToList.BatteryStatus,
                CurrentLocation = droneToList.CurrentLocation,
                Parcel = droneToList.ParcelId != null ? CreateParcelInTransfer((int)droneToList.ParcelId) : null
            };
        }
        private IDAL.DO.Station ClosetStationPossible(IEnumerable<IDAL.DO.Station> stations, DroneToList droneToList,out double minDistance)
        {
            IDAL.DO.Station station = ClosetStation(stations, droneToList.CurrentLocation);
            minDistance = Distance(new Location() { Longitude = station.Longitude, Latitude = station.Latitude }, droneToList.CurrentLocation);
            return minDistance * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE] < droneToList.BatteryStatus ? station : default(IDAL.DO.Station);
        }
        private IDAL.DO.Station ClosetStation(IEnumerable<IDAL.DO.Station> stations, Location location)
        {
            double minDistance = 0;
            double curDistance;
            IDAL.DO.Station station = default;
            foreach (var item in stations)
            {
                curDistance = Distance(location,
                    new Location() { Latitude = item.Latitude, Longitude = item.Longitude });
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    station = item;
                }
            }
            return station;
        }
        private DroneWithParcel mapDroneWithParcel(DroneToList drone)
        {
            return new DroneWithParcel()
            {
                CurrentLocation = drone.CurrentLocation,
                Id = drone.Id,
                ChargingMode = drone.BatteryStatus
            };
        }

        public void DeliveryParcelByDrone(int droneId)
        {
            DroneToList droneToList = drones.Find(item => item.Id == droneId);
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
            if (!parcel.Delivered.Equals(default))
                throw new ArgumentNullException("The package has already been deliverd");
            drones.Remove(droneToList);
            IDAL.DO.Customer customer = dal.GetCustomer(parcel.TargetId);
            Location receiverLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
            droneToList.BatteryStatus -= Distance(droneToList.CurrentLocation, receiverLocation) * dal.GetElectricityUse()[1 + (int)parcel.Weigth];
            droneToList.CurrentLocation = receiverLocation;
            droneToList.DroneStatus = DroneStatuses.AVAILABLE;
            drones.Add(droneToList);
            ParcelDeliveredDrone(parcel.Id);
        }

        public void AssingParcelToDrone(int droneId)
        {
            DroneToList aviableDrone = drones.Find(item => item.Id == droneId);
            Dictionary<ParcelToList,double> parcels = creatParcelListToAssign(aviableDrone);
            ParcelToList parcel=treatInPiority(aviableDrone, parcels, Priorities.EMERGENCY);
            drones.Remove(aviableDrone);
            aviableDrone.DroneStatus = DroneStatuses.DELIVERY;
            aviableDrone.ParcelId = parcel.Id;
            AssigningDroneToParcel(parcel.Id, aviableDrone.Id);
            drones.Add(aviableDrone);

        }
        private ParcelToList treatInPiority( DroneToList aviableDrone, Dictionary<ParcelToList, double> parcels, Priorities priority)
        {
            double minDistance = double.MaxValue;
            WeightCategories weight = WeightCategories.LIGHT;
            ParcelToList parcel = default;
            Priorities maxPriority = Priorities.REGULAR;
            foreach (var item in parcels)
            {
                    if ( maxPriority<item.Key.Piority &&item.Value<= minDistance && item.Key.Weight >= weight )
                    {
                        parcel = item.Key;
                        minDistance =item.Value;
                        weight = item.Key.Weight;
                    }
            }
            return parcel;
        }

    }
}
