using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BL
{
    public partial class BL : IBlDrone
    {
        private const int NUM_OF_MINUTE_IN_HOUR = 60;
        private const int MIN_BATTERY = 20;
        private const int MAX_BATTERY = 40;

        #region Add


        public void AddDrone(Drone droneBl, int stationId)
        {
            try
            {
                DO.Station station;
                lock (dal)
                    dal.AddDrone(droneBl.Id, droneBl.Model, (DO.WeightCategories)droneBl.WeightCategory);
                lock (dal)
                    station = dal.GetStation(stationId);
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
                    dal.AddDroneCharge(droneBl.Id, stationId);
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



        public IEnumerable<DroneToList> GetActiveDrones() => drones.Where(drone => !drone.IsNotActive);


        public IEnumerable<DroneToList> GetAllDrones() => drones;
        #endregion

        #region Update


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


        public void SendDroneForCharg(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new KeyNotFoundException($"The drone id {id} not exsits in data so the updating failed");
            if (droneToList.IsNotActive)
                throw new DeletedExeption("drone deleted", id);
            if (droneToList.DroneState != DroneState.AVAILABLE)
                throw new InvalidDroneStateException($"The drone {id} is {droneToList.DroneState} so it is not possible to send it for charging ");
            try
            {
                Station station = ClosetStationPossible(droneToList.CurrentLocation, (int chargeSlots) => chargeSlots > 0, droneToList.BatteryState, out double minDistance);
                if (station == null)
                {
                    station = ClosetStationPossible(droneToList.CurrentLocation, (int chargeSlots) => true, droneToList.BatteryState, out minDistance);
                    if (station != null)
                    {
                        droneToList.BatteryState -= minDistance * available;
                        droneToList.CurrentLocation = station.Location;
                        throw new NotExsistSuitibleStationException();
                    }
                }
                if (station == null)
                {
                    droneToList.DroneState = DroneState.RESCUE;
                    return;
                }
                droneToList.DroneState = DroneState.MAINTENANCE;
                droneToList.BatteryState -= minDistance * available;
                droneToList.CurrentLocation = station.Location;
                //No charging position was subtracting because there is no point in changing a variable that is not saved after the end of the function
                lock (dal)
                    dal.AddDroneCharge(id, station.Id);
            }
            catch (NotExsistSuitibleStationException)
            {
                droneToList.DroneState = DroneState.RESCUE;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }



        public void ReleaseDroneFromCharging(int id)
        {
            try
            {
                DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
                if (droneToList == default)
                    throw new KeyNotFoundException($"The drone id {id} not exsits in data so the updating failed");
                if (droneToList.IsNotActive)
                    throw new DeletedExeption("drone deleted", id);
                if (droneToList.DroneState != DroneState.MAINTENANCE)
                    throw new InvalidDroneStateException($" The drone is {droneToList.DroneState} so it is not possible to release it form charging ");
                droneToList.IsStopCharge = false;
                droneToList.DroneState = DroneState.AVAILABLE;
                lock (dal)
                    droneToList.BatteryState += (DateTime.Now - dal.GetTimeStartOfCharge(id)).TotalMinutes / NUM_OF_MINUTE_IN_HOUR * droneLoadingRate;
                //No charging position was adding because there is no point in changing a variable that is not saved after the end of the function
                lock (dal)
                    dal.RemoveDroneCharge(id);
            }
            catch (DO.TheDroneIsNotInChargingException ex)
            {
                throw new TheDroneIsNotInChargingException(ex.DroneId);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex);
            }
        }


        public void AssingParcelToDrone(int droneId)
        {
            DroneToList aviableDrone = drones.FirstOrDefault(item => item.Id == droneId);
            try
            {
                if (aviableDrone == default)
                    throw new KeyNotFoundException($"The drone id {droneId} not exsits in data so the updating failed");
                if (aviableDrone.IsNotActive)
                    throw new DeletedExeption("drone deleted", droneId);
                if (aviableDrone.DroneState != DroneState.AVAILABLE)
                    throw new InvalidDroneStateException($" The drone is {aviableDrone.DroneState} so it is not possible to assign it a parcel");
                Dictionary<ParcelToList, double> parcels = CreatParcelDictionaryToAssign(aviableDrone);
                ParcelToList parcel = TreatInPiority(parcels);
                aviableDrone.DroneState = DroneState.DELIVERY;
                aviableDrone.ParcelId = parcel.Id;
                AssigningDroneToParcel(parcel.Id, aviableDrone.Id);
            }
            catch (ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                aviableDrone.DroneState = DroneState.RESCUE;
            }
            catch (NotExsistSutibleParcelException ex)
            {
                throw new NotExsistSutibleParcelException(ex.Message, ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message, ex);
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
                DO.Parcel parcel;
                DO.Customer customer;
                lock (dal)
                  parcel = dal.GetParcel((int)droneToList.ParcelId);
                if (parcel.PickedUp != null)
                    throw new InvalidParcelStateException("The package has already been collected");
                lock (dal)
                    customer = dal.GetCustomer(parcel.SenderId);
                Location senderLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                droneToList.BatteryState -= Distance(droneToList.CurrentLocation, senderLocation) * available;
                droneToList.CurrentLocation = senderLocation;
                ParcelcollectionDrone(parcel);
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
                DO.Parcel parcel;
                lock (dal)
                    parcel = dal.GetParcel((int)droneToList.ParcelId);
                if (parcel.Delivered != null)
                    throw new InvalidParcelStateException("The package has already been deliverd");
                DO.Customer customer;
                lock (dal)
                    customer = dal.GetCustomer(parcel.TargetId);
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
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        #endregion

        #region Delete


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
                lock (dal)
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
            catch (TheDroneIsNotInChargingException)
            {
                drone.DroneState = DroneState.AVAILABLE;
                drone.IsNotActive = true;
            }

        }


        /// <summary>
        /// Delete parcel from drone
        /// </summary>
        /// <param name="id">id of drone</param>
        private void DeleteParcelFromDrone(int id)
        {
            DroneToList drone = drones.FirstOrDefault(item => item.Id == id);
            if (drone != default)
            {
                drone.ParcelId = null;
                drone.DroneState = DroneState.AVAILABLE;
            }
        }
        #endregion



        public bool IsNotActiveDrone(int id) => drones.Any(drone => drone.Id == id && drone.IsNotActive);

        /// <summary>
        /// Create list drone in charging
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>list of drone in chargings</returns>
        private IEnumerable<DroneInCharging> CreatListDroneInCharging(int id)
        {
            IEnumerable<int> list;
            lock (dal)
                list = dal.GetDronechargingInStation((int stationIdOfDrone) => stationIdOfDrone == id);
            if (list.Count() == 0)
                return new List<DroneInCharging>();
            return list.Select(dron => new DroneInCharging() { Id = dron, ChargingMode = drones.FirstOrDefault(item => (item.Id == dron)).BatteryState });
        }

        /// <summary>
        /// Find the best parcel to assigning to thev drone
        /// </summary>
        /// <param name="drone">The drone to assining it</param>
        /// <returns>The best parcel</returns>
        private ParcelToList TreatInPiority(Dictionary<ParcelToList, double> parcels)
        {
            if (!parcels.Any())
                throw new NotExsistSutibleParcelException("There is no suitable parcel that meets all the conditions");
            return parcels.OrderByDescending(parcel => parcel.Key.Piority)
                .ThenByDescending(parcel => parcel.Key.Weight)
                .ThenBy(parcel => parcel.Value)
                .FirstOrDefault().Key;
        }



        public void StartDroneSimulator(int id, Action<int?, int?, int?, int?> update, Func<bool> IsCheckStop)
        {
            DroneToList drone = drones.FirstOrDefault(drone => drone.Id == id);
            if (drone != default)
                drone.IsStopCharge = true;
            new DroneSimulator(id, this, update, IsCheckStop);
        }
    }
}
