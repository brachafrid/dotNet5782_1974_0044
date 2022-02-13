using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BL
{

    public partial class BL : IBlDrone
    {

        private const int NUM_OF_MINUTE_IN_HOUR = 60;
        private const int MIN_BATTERY = 20;
        private const int MAX_BATTERY = 40;
        #region Add
        /// <summary>
        /// Add a drone to the list of drones in data and also convert it to Drone To List and add it to BL list
        /// </summary>
        /// <param name="droneBl">The drone for Adding</param>
        ///<param name="stationId">The station to put the drone</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone droneBl, int stationId)
        {
            try
            {
                lock (dal)
                    dal.AddDrone(droneBl.Id, droneBl.Model, (DO.WeightCategories)droneBl.WeightCategory);
                DO.Station station = dal.GetStation(stationId);
                drones.Add(new()
                {
                    Id = droneBl.Id,
                    DroneModel = droneBl.Model,
                    Weight = droneBl.WeightCategory,
                    BatteryState = rand.NextDouble() + rand.Next(MIN_BATTERY, MAX_BATTERY),
                    DroneState = DroneState.MAINTENANCE,
                    CurrentLocation = new Location() { Latitude = station.Latitude, Longitude = station.Longitude },
                    ParcelId = 0,
                    IsNotActive = false
                });
                lock (dal)
                    dal.AddDRoneCharge(droneBl.Id, stationId);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        #endregion

        #region Return
        /// <summary>
        /// Retrieves the requested drone from the data and converts it to BL drone
        /// </summary>
        /// <param name="id">The requested drone id</param>
        /// <returns>A Bl drone to print</returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone GetDrone(int id)
        {
            try
            {
                return MapDrone(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetActiveDrones() => drones.Where(drone => !drone.IsNotActive);

        /// <summary>
        /// Recrieves the list of drones from BL
        /// </summary>
        /// <returns>A list of drones to print</returns>
        public IEnumerable<DroneToList> GetDrones() => drones;
        #endregion

        #region Update
        /// <summary>
        /// Update a drone in the Stations list
        /// </summary>
        /// <param name="id">The drone to update</param>
        /// <param name="name">The new name</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int id, string name)
        {
            try
            {
                if (name.Equals(string.Empty))
                    throw new ArgumentNullException("For updating the name must be initialized ");
                DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
                if (droneToList == default)
                    throw new KeyNotFoundException($"The drone id {id} not exsits in data so the updating failed");
                lock (dal)
                    dal.UpdateDrone(dal.GetDrone(id), name);
                droneToList.DroneModel = name;
            }

            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Send a drone for charging in the closet station with empty charge slots tha t the drone can arrive there
        /// </summary>
        /// <param name="id">The id of the drone</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneForCharg(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new KeyNotFoundException($"The drone id {id} not exsits in data so the updating failed");
            if (droneToList.IsNotActive)
                throw new DeletedExeption("drone deleted",id);
            if (droneToList.DroneState != DroneState.AVAILABLE && droneToList.DroneState != DroneState.WAYTOCHARGE)
                throw new InvalidDroneStateException($"The drone {id} is {droneToList.DroneState} so it is not possible to send it for charging ");
            try
            {
                Station station = ClosetStationPossible(droneToList.CurrentLocation, (int chargeSlots) => chargeSlots > 0, droneToList.BatteryState, out double minDistance);
                if (station == null)
                    throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException();
                droneToList.DroneState = DroneState.MAINTENANCE;
                droneToList.BatteryState -= minDistance * available;
                droneToList.CurrentLocation = station.Location;
                //No charging position was subtracting because there is no point in changing a variable that is not saved after the end of the function
                dal.AddDRoneCharge(id, station.Id);
            }
            catch (NotExsistSuitibleStationException ex)
            {
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException(ex.Message, ex);//?
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Realse the drone from charging
        /// </summary>
        /// <param name="id">The drone to realsing</param>
        /// <param name="timeOfCharg">The time of charging</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromCharging(int id)
        {
            try
            {
                DroneToList  droneToList = drones.FirstOrDefault(item => item.Id == id);
                if (droneToList == default)
                    throw new KeyNotFoundException($"The drone id {id} not exsits in data so the updating failed");
                if (droneToList.IsNotActive)
                    throw new DeletedExeption("drone deleted", id);
                if (droneToList.DroneState != DroneState.MAINTENANCE)
                    throw new InvalidDroneStateException($" The drone is {droneToList.DroneState} so it is not possible to release it form charging ");
                droneToList.DroneState = DroneState.AVAILABLE;
                droneToList.BatteryState += (DateTime.Now - dal.GetTimeStartOfCharge(id)).TotalMinutes / NUM_OF_MINUTE_IN_HOUR * droneLoadingRate;
                //No charging position was adding because there is no point in changing a variable that is not saved after the end of the function
                dal.RemoveDroneCharge(id);
            }
            catch (DO.TheDroneIsNotInChargingException ex)
            {
                throw new TheDroneIsNotInChargingException(ex.DroneId);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Assign parcel to drone in according to weight and distance (call to help function)
        /// </summary>
        /// <param name="droneId">The drone to assign it a parcel</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssingParcelToDrone(int droneId)
        {
            try
            {
                DroneToList aviableDrone = drones.FirstOrDefault(item => item.Id == droneId);
                if (aviableDrone == default)
                    throw new KeyNotFoundException($"The drone id {droneId} not exsits in data so the updating failed");
                if (aviableDrone.IsNotActive)
                    throw new DeletedExeption("drone deleted", droneId);
                if (aviableDrone.DroneState != DroneState.AVAILABLE && aviableDrone.DroneState!=DroneState.WAYTOCHARGE)
                    throw new InvalidDroneStateException($" The drone is {aviableDrone.DroneState} so it is not possible to assign it a parcel");
                Dictionary<ParcelToList, double> parcels = CreatParcelDictionaryToAssign(aviableDrone);
                ParcelToList parcel = TreatInPiority(parcels);
                aviableDrone.DroneState = DroneState.DELIVERY;
                aviableDrone.ParcelId = parcel.Id;
                AssigningDroneToParcel(parcel.Id, aviableDrone.Id);
            }
            catch (ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException("",ex);//?
            }
            catch(NotExsistSutibleParcelException ex)
            {
                throw new NotExsistSutibleParcelException(ex.Message,ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message,ex);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Collecting the parcel from the sender 
        /// </summary>
        /// <param name="droneId">The drone that collect</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelCollectionByDrone(int droneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == droneId);
            if (droneToList == default)
                throw new KeyNotFoundException($"The drone id {droneId} not exsits in data so the updating failed");
            if (droneToList.IsNotActive)
                throw new DeletedExeption("drone deleted", droneId);
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            try
            {
                DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
                if (parcel.PickedUp != null)
                    throw new InvalidParcelStateException("The package has already been collected");
                DO.Customer customer = dal.GetCustomer(parcel.SenderId);
                Location senderLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                droneToList.BatteryState -= Distance(droneToList.CurrentLocation, senderLocation) * available;
                droneToList.CurrentLocation = senderLocation;
                ParcelcollectionDrone(parcel);

            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch(DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }


        }

        /// <summary>
        /// Deliverd the parcel to the reciver 
        /// </summary>
        /// <param name="droneId">The drone that deliverd</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryParcelByDrone(int droneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == droneId);
            if (droneToList == default)
                throw new KeyNotFoundException($"The drone id {droneId} not exsits in data so the updating failed");
            if (droneToList.IsNotActive)
                throw new DeletedExeption("drone deleted", droneId);
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");

            try
            {
                DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
                if (parcel.Delivered != null)
                    throw new InvalidParcelStateException("The package has already been deliverd");
                DO.Customer customer = dal.GetCustomer(parcel.TargetId);
                Location receiverLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                droneToList.BatteryState -= Distance(droneToList.CurrentLocation, receiverLocation) * (WeightCategories)parcel.Weigth switch
                {
                    WeightCategories.LIGHT => lightWeightCarrier,
                    WeightCategories.MEDIUM => mediumWeightBearing,
                    WeightCategories.HEAVY => carriesHeavyWeight,
                    _ => 0
                };
                droneToList.CurrentLocation = receiverLocation;
                droneToList.DroneState = DroneState.AVAILABLE;
                droneToList.ParcelId = 0;
                ParcelDeliveredDrone(parcel);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch( DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        #endregion

        #region Delete
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            DroneToList drone = drones.FirstOrDefault(item => item.Id == id);
            try
            {
                if (drone == default)
                    throw new KeyNotFoundException($"the drone id {id} not exsits in data ");
                if (drone.IsNotActive)
                    return;
                if (drone.DroneState == DroneState.MAINTENANCE)
                    ReleaseDroneFromCharging(drone.Id);
                dal.DeleteDrone(id);
                drone.IsNotActive = true;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
            catch(TheDroneIsNotInChargingException)
            {
                drone.DroneState = DroneState.AVAILABLE;
                drone.IsNotActive = true;
            }

        }
        #endregion
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsNotActiveDrone(int id) => drones.Any(drone => drone.Id == id && drone.IsNotActive);

        private IEnumerable<DroneInCharging> CreatListDroneInCharging(int id)
        {
            IEnumerable<int> list;
            lock (dal)
                list = dal.GetDronechargingInStation((int stationIdOfDrone) => stationIdOfDrone == id);
            if (list.Count() == 0)
                return new List<DroneInCharging>();
            List<DroneInCharging> droneInChargings = new();
            DroneToList droneToList;
            foreach (var idDrone in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.Id == idDrone));
                if (droneToList != default)
                {
                    droneInChargings.Add(new DroneInCharging() { Id = idDrone, ChargingMode = droneToList.BatteryState });
                }
            }
            return droneInChargings;
        }
     
       
        /// <summary>
        /// Find the best parcel to assigning to thev drone
        /// </summary>
        /// <param name="drone">The drone to assining it</param>
        /// <returns>The best parcel</returns>
        private ParcelToList TreatInPiority(Dictionary<ParcelToList, double> parcels)
        {
            var orderdParcel = parcels.OrderByDescending(parcel => parcel.Key.Piority).ThenByDescending(parcel => parcel.Key.Weight).ThenBy(parcel => parcel.Value).ToDictionary(item => item.Key, item => item.Value);
            if (!orderdParcel.Any())
                throw new NotExsistSutibleParcelException("There is no suitable parcel that meets all the conditions");
            ParcelToList suitableParcel = orderdParcel.FirstOrDefault().Key;
            return suitableParcel;
        }

       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void StartDroneSimulator(int id, Action<int?, int?, int?, int?> update, Func<bool> checkStop)
        {
            new DroneSimulator(id, this, update, checkStop);
        }

        
    }
}
