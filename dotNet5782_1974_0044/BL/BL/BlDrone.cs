using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBL
{
    public partial class BL : IblDrone
    {
        public void AddDrone(int id, string model, BO.WeightCategories MaximumWeight, int stationId)
        {

            throw new NotImplementedException();
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
            if (droneToList.ParcellId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcellId);
            if (!parcel.PickedUp.Equals(default(DateTime)))
                throw new ArgumentNullException("The package has already been collected");
            drones.Remove(droneToList);
            IDAL.DO.Customer customer = dal.GetCustomer(parcel.SenderId);
            Location senderLocation = new Location() { Longitude = customer.Longitude, Latitude = customer.Latitude };
            droneToList.BatteryStatus -= Distance(droneToList.CurrentLocation, senderLocation)*dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            droneToList.CurrentLocation = senderLocation;
            drones.Add(droneToList);
            ParcelcollectionDrone(parcel.Id);
        }
        public void ReleaseDroneFromCharging(int id, float timeOfCharg)
        {
            DroneToList droneToList = drones.Find(item => item.Id == id);
            if (droneToList.DroneStatus != DroneStatuses.MAINTENANCE)
                throw new Exception();
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatuses.AVAILABLE;
            droneToList.BatteryStatus += timeOfCharg / 60 * dal.GetElectricityUse()[4];
            dal.RemoveDroneCharge(id);
        }

        public void SendDroneForCharg(int id)
        {
            DroneToList droneToList = drones.Find(item => item.Id == id);
            if (droneToList.DroneStatus != DroneStatuses.AVAILABLE)
                throw new Exception();
            double minDistance;
            IDAL.DO.Station station = ClosetStationPossible(dal.GetStations(), droneToList, out minDistance);
            if (station.Equals(default(IDAL.DO.Station)))
                throw new Exception();
            drones.Remove(droneToList);
            droneToList.CurrentLocation = new Location() { Longitude = station.Longitude, Latitude = station.Latitude };
            droneToList.DroneStatus = DroneStatuses.MAINTENANCE;
            droneToList.BatteryStatus -= minDistance * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            //הורדת מספר עמדות טעינה בתחנה
            dal.AddDRoneCharge(id, station.Id);
        }
        public void UpdateDrone(int id, string name)
        {
            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Drone droneDl = dal.GetDrone(id);
            if (name.Equals(default(string)))
                throw new ArgumentNullException("For updating the name must be initialized ");
            dal.RemoveDrone(droneDl);
            dal.addDrone(id, name, droneDl.MaxWeight);
            DroneToList droneToList = drones.Find(item => item.Id == id);
            drones.Remove(droneToList);
            droneToList.DroneModel = name;
            drones.Add(droneToList);
        }
        public IEnumerable<DroneToList> GetDrones() => drones;
        private List<DroneInCharging> CreatListDroneInCharging(int id)
        {
            List<int> list = dal.GetDronechargingInStation(id);
            List<DroneInCharging> droneInChargings = new List<DroneInCharging>();
            DroneToList droneToList;
            foreach (var idDrone in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.Id == idDrone));
                if (!droneToList.Equals(default(DroneToList)))
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
        private IDAL.DO.Station ClosetStationPossible(IEnumerable<IDAL.DO.Station> stations, DroneToList droneToList, out double minDistance)
        {
            IDAL.DO.Station station = ClosetStation(stations, droneToList, out minDistance);
            return minDistance * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE] < droneToList.BatteryStatus ? station : default(IDAL.DO.Station);
        }
        private IDAL.DO.Station ClosetStation(IEnumerable<IDAL.DO.Station> stations, DroneToList droneToList, out double minDistance)
        {
            minDistance = 0;
            double curDistance;
            IDAL.DO.Station station = default(IDAL.DO.Station);
            foreach (var item in stations)
            {
                curDistance = Distance(droneToList.CurrentLocation,
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
            if (droneToList.ParcellId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcellId);
            if (!parcel.Delivered.Equals(default(DateTime)))
                throw new ArgumentNullException("The package has already been deliverd");
            drones.Remove(droneToList);
            IDAL.DO.Customer customer = dal.GetCustomer(parcel.TargetId);
            Location receiverLocation = new Location() { Longitude = customer.Longitude, Latitude = customer.Latitude };
            droneToList.BatteryStatus -= Distance(droneToList.CurrentLocation, receiverLocation) * dal.GetElectricityUse()[1 + (int)parcel.Weigth];
            droneToList.CurrentLocation = receiverLocation;
            droneToList.DroneStatus = DroneStatuses.AVAILABLE;
            drones.Add(droneToList);
            ParcelcollectionDrone(parcel.Id);
        }

        public void AssingParcellToDrone(int droneId)
        {
            DroneToList aviableDrone = drones.Find(item => item.Id == droneId);
            List<ParcelInTransfer> parcels = (List<ParcelInTransfer>)CreateParcelInTransferList(aviableDrone.Weight);
            treatInPiority(ref aviableDrone, parcels, Priorities.EMERGENCY);
            if (!(aviableDrone.DroneStatus == DroneStatuses.DELIVERY))
            {
                treatInPiority(ref aviableDrone, parcels, Priorities.FAST);
                if (!(aviableDrone.DroneStatus == DroneStatuses.DELIVERY))
                    treatInPiority(ref aviableDrone, parcels, Priorities.REGULAR);
            }


        }
        private void treatInPiority(ref DroneToList aviableDrone, List<ParcelInTransfer> parcels, Priorities piority)
        {
            double minDistance = double.MaxValue, tmpDistance;
            WeightCategories weight = WeightCategories.LIGHT;
            ParcelInTransfer parcel = default(ParcelInTransfer);
            foreach (var item in parcels)
            {
                if (item.Priority == piority)
                {
                    if (calculateElectricity(aviableDrone, item, item.WeightCategory, out tmpDistance) <= aviableDrone.BatteryStatus && tmpDistance <= minDistance && item.WeightCategory >= weight)
                    {
                        parcel = item;
                        minDistance = tmpDistance;
                        weight = item.WeightCategory;
                    }
                }
            }
            treatInDrone(ref aviableDrone, parcel);
        }

        private void treatInDrone(ref DroneToList aviableDrone, ParcelInTransfer parcel)
        {
            drones.Remove(aviableDrone);
            aviableDrone.DroneStatus = DroneStatuses.DELIVERY;
            aviableDrone.ParcellId = parcel.Id;
            AssigningDroneToParcel(parcel.Id, aviableDrone.Id);
            drones.Add(aviableDrone);
        }
        private double calculateElectricity(DroneToList aviableDrone, ParcelInTransfer parcel, WeightCategories status, out double minDistance)
        {
            DroneToList tempDrone = aviableDrone;
            double electricity;
            IDAL.DO.Station station;
            electricity = Distance(aviableDrone.CurrentLocation, parcel.CollectionPoint) * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE] +
                        Distance(parcel.CollectionPoint, parcel.DeliveryDestination) * dal.GetElectricityUse()[(int)status + 1];
            tempDrone.BatteryStatus -= electricity;
            station = ClosetStationPossible(dal.GetStations(), tempDrone, out minDistance);
            electricity += Distance(parcel.DeliveryDestination,
                         new Location() { Latitude = station.Latitude, Longitude = station.Longitude }) * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            return electricity;
        }

    }
}
