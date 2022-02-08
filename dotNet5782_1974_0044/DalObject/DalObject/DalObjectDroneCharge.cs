using DLApi;
using System.Collections.Generic;
using System;
using System.Linq;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalObject:IDalDroneCharge
    {
        /// <summary>
        /// Count a number of charging slots occupied at a particular station 
        /// </summary>
        /// <param name="id">the id number of a station</param>
        /// <returns>The counter of empty slots</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int CountFullChargeSlots(int id)=> DalObjectService.GetEntities<DroneCharge>().Count(Drone => Drone.Stationld == id);
        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        ///<param name="inTheStation">The predicate to screen out if the station id of the drone equal to a spesific station id </param>
        /// <returns>A list of DroneCarge</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation)
        {
            return DalObjectService.GetEntities<DroneCharge>().Where(item => inTheStation(item.Stationld)).Select(item => item.Droneld);
        }
        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDRoneCharge(int droneId, int stationId)
        {
            DalObjectService.AddEntity(new DroneCharge() { Droneld = droneId, Stationld = stationId, StartCharging = DateTime.Now });
        }
        /// <summary>
        /// Remove DroneCharge object from the list
        /// </summary>
        /// <param name="droneId">The drone to remove</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDroneCharge(int droneId)
        {
            DroneCharge droneCharge = DalObjectService.GetEntities<DroneCharge>().FirstOrDefault(droneCharge => droneCharge.Droneld == droneId);
            if (droneCharge.Equals(default))
                throw new TheDroneIsNotInChargingException("The drone is not in charging",droneId);
            DalObjectService.RemoveEntity(droneCharge);
        }

        /// <summary>
        /// Get the time of start charging
        /// </summary>
        /// <param name="droneId">The drone in charging</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DateTime GetTimeStartOfCharge(int droneId)
        {
            DroneCharge droneCharge = DalObjectService.GetEntities<DroneCharge>().FirstOrDefault(drone => drone.Droneld == droneId);
            if (droneCharge.Equals(default))
                throw new TheDroneIsNotInChargingException("The drone is not in charging", droneId);
            return droneCharge.StartCharging;
        }
        /// <summary>
        /// Takes from the DataSource the electricity use data of the drone
        /// </summary>
        /// <returns>A array of electricity use</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public (double, double, double, double, double) GetElectricity()
        {
            return (DataSorce.Config.Available, DataSorce.Config.LightWeightCarrier, DataSorce.Config.MediumWeightBearing, DataSorce.Config.CarriesHeavyWeight, DataSorce.Config.DroneLoadingRate);

        }
    }

}
