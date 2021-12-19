using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;


namespace IBL
{
    
    public partial class BL : IBlDrone
    {
        //
        private const int NUM_OF_MINUTE_IN_HOUR = 60;
        private const int MIN_BATTERY = 20;
        private const int MAX_BATTERY = 40;
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
                    BatteryState = rand.NextDouble() + rand.Next(MIN_BATTERY, MAX_BATTERY),
                    DroneState = DroneState.MAINTENANCE,
                    CurrentLocation = new Location() { Latitude = station.Latitude, Longitude = station.Longitude },
                    ParcelId = 0
                };
                drones.Add(droneToList);
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {

                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
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

                throw new ArgumentNullException( ex.Message);
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
          
            DroneToList droneToList = default;
            try
            {
                IDAL.DO.Drone droneDl = dal.GetDrone(id);
                dal.RemoveDrone(droneDl);
                dal.AddDrone(id, name, droneDl.MaxWeight);
                droneToList = drones.First(item => item.Id == id);
                drones.Remove(droneToList);
                droneToList.DroneModel = name;
                if (name.Equals(string.Empty))
                    throw new ArgumentNullException("For updating the name must be initialized ");
            }
          
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException( ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException( ex.Message);
            }
            finally
            {
                if(droneToList != default)
                    drones.Add(droneToList);
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
                throw new ArgumentNullException(" There is no a drone with the same id in data");
            if (droneToList.DroneState != DroneState.AVAILABLE)
                throw new InvalidEnumArgumentException("The drone is not available so it is not possible to send it for charging ");
            IDAL.DO.Station station = ClosetStationPossible(dal.GetStations(), droneToList.CurrentLocation, droneToList.BatteryState, out double minDistance);
            if (station.Equals(default(IDAL.DO.Station)))
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException();
            drones.Remove(droneToList);
            droneToList.DroneState = DroneState.MAINTENANCE;
            droneToList.BatteryState -= minDistance * available;
            droneToList.CurrentLocation = new Location() { Longitude = station.Longitude, Latitude = station.Latitude }; ;
            //No charging position was subtracting because there is no point in changing a variable that is not saved after the end of the function
            dal.AddDRoneCharge(id, station.Id);
            drones.Add(droneToList);
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
                throw new ArgumentNullException("There is no a drone with the same id in charging");
            if (droneToList.DroneState != DroneState.MAINTENANCE)
                throw new InvalidEnumArgumentException(" The drone is not maintenace so it is not possible to release it form charging ");
            drones.Remove(droneToList);
            droneToList.DroneState = DroneState.AVAILABLE;
            droneToList.BatteryState += timeOfCharg / NUM_OF_MINUTE_IN_HOUR * droneLoadingRate;
            //No charging position was adding because there is no point in changing a variable that is not saved after the end of the function
            dal.RemoveDroneCharge(id);
            drones.Add(droneToList);
        }

        /// <summary>
        /// Assign parcel to drone in according to weight and distance (call to help function)
        /// </summary>
        /// <param name="droneId">The dreone to assign it a parcel</param>
        public void AssingParcelToDrone(int droneId)
        {
            DroneToList aviableDrone = drones.FirstOrDefault(item => item.Id == droneId);
            if (aviableDrone == default)
                throw new ArgumentNullException(" There is no a drone with the same id in data");
            if (aviableDrone.DroneState != DroneState.AVAILABLE)
                throw new InvalidEnumArgumentException(" The drone is not aviable so it is not possible to assign it a parcel");
            Dictionary<ParcelToList, double> parcels = CreatParcelDictionaryToAssign(aviableDrone);
            try
            {
                ParcelToList parcel = TreatInPiority(parcels);
                drones.Remove(aviableDrone);
                aviableDrone.DroneState = DroneState.DELIVERY;
                aviableDrone.ParcelId = parcel.Id;
                AssigningDroneToParcel(parcel.Id, aviableDrone.Id);

            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            finally
            {
                drones.Add(aviableDrone);
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
                throw new ArgumentNullException(" There is no a drone with the same id in data");
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            drones.Remove(droneToList);
            IDAL.DO.Parcel parcel = default;
            try
            {
                 parcel= dal.GetParcel((int)droneToList.ParcelId);
                if (parcel.PickedUp!= null)
                    throw new ArgumentNullException("The package has already been collected");
                IDAL.DO.Customer customer = dal.GetCustomer(parcel.SenderId);
                Location senderLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                droneToList.BatteryState -= Distance(droneToList.CurrentLocation, senderLocation) * available;
                droneToList.CurrentLocation = senderLocation;
                
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException( ex.Message);
            }
            finally
            {
                drones.Add(droneToList);
                if(!parcel.Equals(default(IDAL.DO.Parcel)))
                    ParcelcollectionDrone(parcel.Id);
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
                throw new ArgumentNullException("There is no a drone with the same id in data");
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
            if (parcel.Delivered!=null)
                throw new ArgumentNullException("The package has already been deliverd");
            drones.Remove(droneToList);
            try
            {
                IDAL.DO.Customer customer = dal.GetCustomer(parcel.TargetId);
                Location receiverLocation = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                droneToList.BatteryState -= Distance(droneToList.CurrentLocation, receiverLocation) * (WeightCategories)parcel.Weigth switch
                {
                    WeightCategories.LIGHT => lightWeightCarrier,
                    WeightCategories.MEDIUM => mediumWeightBearing,
                    WeightCategories.HEAVY => carriesHeavyWeight
                };
                droneToList.CurrentLocation = receiverLocation;
                droneToList.DroneState = DroneState.AVAILABLE;
                droneToList.ParcelId = 0;
                ParcelDeliveredDrone(parcel.Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException( ex.Message);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            finally
            {
                drones.Add(droneToList);
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
            List<int> list = dal.GetDronechargingInStation((int stationIdOfDrone)=> stationIdOfDrone == id);
            if (list.Count == 0)
                return new();
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
                throw new ArgumentNullException("Map drone: There is not drone with same id in the data");
            return new Drone()
            {
                Id = droneToList.Id,
                Model = droneToList.DroneModel,
                WeightCategory = droneToList.Weight,
                DroneState = droneToList.DroneState,
                BattaryMode = droneToList.BatteryState,
                CurrentLocation = droneToList.CurrentLocation,
                Parcel = droneToList.ParcelId !=0 ? CreateParcelInTransfer((int)droneToList.ParcelId) : null
            };
        }
        //InvalidOperationException

        /// <summary>
        /// Find the best parcel to assigning to thev drone
        /// </summary>
        /// <param name="drone">The drone to assining it</param>
        /// <returns>The best parcel</returns>
        private ParcelToList TreatInPiority(Dictionary<ParcelToList, double> parcels)
        {
            var orderdParcel = parcels.OrderByDescending(parcel => parcel.Key.Piority).ThenByDescending(parcel => parcel.Key.Weight).ThenBy(parcel => parcel.Value).ToDictionary(item => item.Key, item => item.Value);
            if (!orderdParcel.Any())
                throw new KeyNotFoundException("There is no suitable parcel that meets all the conditions");
            ParcelToList suitableParcel = orderdParcel.FirstOrDefault().Key;
            return suitableParcel;
        }

    }
}
