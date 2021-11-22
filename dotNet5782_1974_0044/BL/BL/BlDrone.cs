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
            try
            {
                dal.AddDrone(droneBl.Id, droneBl.Model, (IDAL.DO.WeightCategories)droneBl.WeightCategory);
                IDAL.DO.Station station = dal.GetStation(stationId);
                DroneToList droneToList = new()
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
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {

                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException("Add drone -BL-"+ex.Message);
            }

        }

        //--------------------------------------------------Return-----------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested drone from the data and converts it to BL drone
        /// </summary>
        /// <param name="id">The requested drone id</param>
        /// <returns>A Bl drone to print</returns>
        public BO.Drone GetDrone(int id)
        {
            try
            {
                return MapDrone(id);
            }
            catch (ArgumentNullException ex)
            {

                throw new ArgumentNullException("Get drone -BL-"+ex.Message);
            }
            
        }

        //--------------------------------------------------Updating-----------------------------------------------------------------------------------

        /// <summary>
        /// Update a drone in the Stations list
        /// </summary>
        /// <param name="id">The drone to update</param>
        /// <param name="name">The new name</param>
        public void UpdateDrone(int id, string name)
        {
            if (name.Equals(default))
                throw new ArgumentNullException("Update drone -BL-:For updating the name must be initialized ");
            try
            {
                IDAL.DO.Drone droneDl = dal.GetDrone(id);
                dal.RemoveDrone(droneDl);
                dal.AddDrone(id, name, droneDl.MaxWeight);
                DroneToList droneToList = drones.Find(item => item.Id == id);
                drones.Remove(droneToList);
                droneToList.DroneModel = name;
                drones.Add(droneToList);
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Update drone -BL-"+ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Update drone -BL-"+ex.Message);
            }

        }

        /// <summary>
        /// Send a drone for charging in the closet station with empty charge slots tha t the drone can arrive there
        /// </summary>
        /// <param name="id">The id of the drone</param>
        public void SendDroneForCharg(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new ArgumentNullException("Send Drone For Charg -BL-: There is no a drone with the same id in data");
            if (droneToList.DroneStatus != DroneStatuses.AVAILABLE)
                throw new InvalidEnumArgumentException("Send Drone For Charg -BL-: The drone is not available so it is not possible to send it for charging ");
            double minDistance;
            IDAL.DO.Station station = ClosetStationPossible(dal.GetStations(), droneToList.CurrentLocation,droneToList.BatteryStatus, out minDistance);
            if (station.Equals(default))
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException("Send Drone For Charg -BL-");
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
                throw new ArgumentNullException("Release Drone Form Charging -BL-: There is no a drone with the same id in charging");
            if (droneToList.DroneStatus != DroneStatuses.MAINTENANCE)
                throw new InvalidEnumArgumentException("Release Drone Form Charging -BL-: The drone is not maintenace so it is not possible to release it form charging ");
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
                throw new ArgumentNullException("Assing Parcel To Drone -BL-: There is no a drone with the same id in data");
            Dictionary<ParcelToList, double> parcels = creatParcelListToAssign(aviableDrone);
            try
            {
                ParcelToList parcel = treatInPiority(aviableDrone, parcels, Priorities.EMERGENCY);
                drones.Remove(aviableDrone);
                aviableDrone.DroneStatus = DroneStatuses.DELIVERY;
                aviableDrone.ParcelId = parcel.Id;
                AssigningDroneToParcel(parcel.Id, aviableDrone.Id);
                drones.Add(aviableDrone);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch(ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }

        }

        /// <summary>
        /// Collecting the parcel from the sender 
        /// </summary>
        /// <param name="DroneId">The drone that collect</param>
        public void ParcelCollectionByDrone(int DroneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == DroneId);
            if (droneToList == default)
                throw new ArgumentNullException("Parcel Collection By Drone -BL-: There is no a drone with the same id in data");
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("Parcel Collection By Drone -BL-:No parcel has been associated yet");
            drones.Remove(droneToList);
            try
            {
                IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
                if (!parcel.PickedUp.Equals(default))
                    throw new ArgumentNullException("Parcel Collection By Drone -BL-:The package has already been collected");
                IDAL.DO.Customer customer = dal.GetCustomer(parcel.SenderId);
                Location senderLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                droneToList.BatteryStatus -= Distance(droneToList.CurrentLocation, senderLocation) * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
                droneToList.CurrentLocation = senderLocation;
                drones.Add(droneToList);
                ParcelcollectionDrone(parcel.Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message+ "Parcel Collection By Drone -BL-");
            }
            catch(ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message+ "Parcel Collection By Drone -BL");
            }

        }

        /// <summary>
        /// Deliverd the parcel to the reciver 
        /// </summary>
        /// <param name="droneId">The drone that deliverd</param>
        public void DeliveryParcelByDrone(int droneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == droneId);
            if (droneToList == default)
                throw new ArgumentNullException("Delivery Parcel By Drone -BL-: There is no a drone with the same id in data");
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("Delivery Parcel By Drone -BL-:No parcel has been associated yet");
            IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
            if (!parcel.Delivered.Equals(default))
                throw new ArgumentNullException("Delivery Parcel By Drone -BL-:The package has already been deliverd");
            drones.Remove(droneToList);
            try
            {
                IDAL.DO.Customer customer = dal.GetCustomer(parcel.TargetId);
                Location receiverLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                droneToList.BatteryStatus -= Distance(droneToList.CurrentLocation, receiverLocation) * dal.GetElectricityUse()[1 + (int)parcel.Weigth];
                droneToList.CurrentLocation = receiverLocation;
                droneToList.DroneStatus = DroneStatuses.AVAILABLE;
                drones.Add(droneToList);
                ParcelDeliveredDrone(parcel.Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message + "Delivery Parcel By Drone -BL-");
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }

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
            if (list.Count == 0)
                return new();
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
                throw new ArgumentNullException("Map drone -BL-:There is not drone with same id i data");
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
            ParcelToList suitableParcel= parcels.Keys.FirstOrDefault();
            if (suitableParcel == default)
                throw new KeyNotFoundException("Assing drone to parcel -BL-:There is no suitable parcel that meets all the conditions");
            return suitableParcel;
        }

    }
}
