using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;


namespace IBL
{
    public partial class BL : IblDrone
    {
        //-----------------------------------------------------------Adding------------------------------------------------------------------------
        /// <summary>
        /// Add a drone to the list of drones in data and also convert it to Drone To List and add it to BL list
        /// </summary>
        /// <param name="droneBl">The drone for Adding</param>
        ///<param name="stationId">The station to put the drone</param>
        public void AddDrone(Drone droneBl, int stationId)
        {
            if (ExistsIDTaxCheck(dal.GetDrones(), droneBl.Id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException();
            if (!ExistsIDTaxCheck(dal.GetStations(), stationId))
                throw new KeyNotFoundException("station not exist");
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

        //--------------------------------------------------Return-----------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested drone from the data and converts it to BL drone
        /// </summary>
        /// <param name="id">The requested drone id</param>
        /// <returns>A Bl drone to print</returns>
        public BO.Drone GetDrone(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            return MapDrone(id);
        }

        //--------------------------------------------------Updating-----------------------------------------------------------------------------------

        /// <summary>
        /// Update a drone in the Stations list
        /// </summary>
        /// <param name="id">The drone to update</param>
        /// <param name="name">The new name</param>
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

        /// <summary>
        /// Send a drone for charging in the closet station with empty charge slots tha t the drone can arrive there
        /// </summary>
        /// <param name="id">The id of the drone</param>
        public void SendDroneForCharg(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new ArgumentNullException();
            if (droneToList.DroneStatus != DroneStatuses.AVAILABLE)
                throw new InvalidEnumArgumentException();
            double minDistance;
            IDAL.DO.Station station = ClosetStationPossible(dal.GetStations(), droneToList, out minDistance);
            if (station.Equals(default))
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException();
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatuses.MAINTENANCE;
            droneToList.BatteryStatus -= minDistance * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            droneToList.CurrentLocation = new Location() { Longitude = station.Longitude, Latitude = station.Latitude }; ;
            //No charging position was subtracting because there is no point in changing a variable that is not saved after the end of the function
            dal.AddDRoneCharge(id, station.Id);
        }

        /// <summary>
        /// REalse the drone from charging
        /// </summary>
        /// <param name="id">The drone to realsing</param>
        /// <param name="timeOfCharg">The time of charging</param>
        public void ReleaseDroneFromCharging(int id, float timeOfCharg)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new ArgumentNullException();
            if (droneToList.DroneStatus != DroneStatuses.MAINTENANCE)
                throw new InvalidEnumArgumentException();
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatuses.AVAILABLE;
            droneToList.BatteryStatus += timeOfCharg / 60 * dal.GetElectricityUse().Last();
            //No charging position was adding because there is no point in changing a variable that is not saved after the end of the function
            dal.RemoveDroneCharge(id);
        }

        /// <summary>
        /// Assign parcel to drone in according to weight and distance (call to help function)
        /// </summary>
        /// <param name="droneId">The dreone to assign it a parcel</param>
        public void AssingParcelToDrone(int droneId)
        {
            DroneToList aviableDrone = drones.FirstOrDefault(item => item.Id == droneId);
            if (aviableDrone == default)
                throw new ArgumentNullException();
            Dictionary<ParcelToList, double> parcels = creatParcelListToAssign(aviableDrone);
            ParcelToList parcel = treatInPiority(aviableDrone, parcels, Priorities.EMERGENCY);
            drones.Remove(aviableDrone);
            aviableDrone.DroneStatus = DroneStatuses.DELIVERY;
            aviableDrone.ParcelId = parcel.Id;
            AssigningDroneToParcel(parcel.Id, aviableDrone.Id);
            drones.Add(aviableDrone);

        }

        /// <summary>
        /// Collecting the parcel from the sender 
        /// </summary>
        /// <param name="DroneId">The drone that collect</param>
        public void ParcelCollectionByDrone(int DroneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == DroneId);
            if (droneToList == default)
                throw new ArgumentNullException();
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

        /// <summary>
        /// Deliverd the parcel to the reciver 
        /// </summary>
        /// <param name="droneId">The drone that deliverd</param>
        public void DeliveryParcelByDrone(int droneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == droneId);
            if (droneToList == default)
                throw new ArgumentNullException();
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


        //-------------------------------------------------Return List-----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of drones from BL
        /// </summary>
        /// <returns>A list of drones to print</returns>
        public IEnumerable<DroneToList> GetDrones() => drones;
        private List<DroneInCharging> CreatListDroneInCharging(int id)
        {
            List<int> list = dal.GetDronechargingInStation(id);
            List<DroneInCharging> droneInChargings = new();
            DroneToList droneToList;
            foreach (var idDrone in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.Id == idDrone));
                if (droneToList!=default)
                {
                    droneInChargings.Add(new DroneInCharging() { Id = idDrone, ChargingMode = droneToList.BatteryStatus });
                }
            }
            return droneInChargings;
        }
        //--------------------------------------------------Help function-----------------------------------------------------------------------------------
        /// <summary>
        /// Convert a Bl Drone To List to BL drone
        /// </summary>
        /// <param name="drone">The drone to convert</param>
        /// <returns>The converted drone</returns>
        private Drone MapDrone(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new ArgumentNullException();
            return new Drone()
            {
                Id = droneToList.Id,
                Model = droneToList.DroneModel,
                WeightCategory = droneToList.Weight,
                DroneStatus = droneToList.DroneStatus,
                BattaryMode = droneToList.BatteryStatus,
                CurrentLocation = droneToList.CurrentLocation,
                Parcel = droneToList.ParcelId != null ? CreateParcelInTransfer((int)droneToList.ParcelId) : null
            };
        }

        /// <summary>
        /// Find the best parcel to assigning to thev drone
        /// </summary>
        /// <param name="drone">The drone to assining it</param>
        /// <returns>The best parcel</returns>
        private ParcelToList treatInPiority( DroneToList aviableDrone, Dictionary<ParcelToList, double> parcels, Priorities priority)
        {
            parcels.OrderByDescending(parcel => parcel.Key.Piority).ThenByDescending(parcel => parcel.Key.Weight).ThenBy(parcel => parcel.Value);
            return parcels.Keys.First();
        }

    }
}
