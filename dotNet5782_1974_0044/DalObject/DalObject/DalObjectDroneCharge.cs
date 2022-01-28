
using System.Collections.Generic;
using System;
using System.Linq;
using DO;


namespace Dal
{
    public partial class DalObject
    {
        /// <summary>
        /// Count a number of charging slots occupied at a particular station 
        /// </summary>
        /// <param name="id">the id number of a station</param>
        /// <returns>The counter of empty slots</returns>
        public int CountFullChargeSlots(int id)=> GetEntities<DroneCharge>().Count(Drone => Drone.Stationld == id);
        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        ///<param name="inTheStation">The predicate to screen out if the station id of the drone equal to a spesific station id </param>
        /// <returns>A list of DroneCarge</returns>
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation)
        {
            return GetEntities<DroneCharge>().Where(item => inTheStation(item.Stationld)).Select(item => item.Droneld);
        }
        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        public void AddDRoneCharge(int droneId, int stationId)
        {
            AddEntity(new DroneCharge() { Droneld = droneId, Stationld = stationId, StartCharging = DateTime.Now });
        }
        /// <summary>
        /// Remove DroneCharge object from the list
        /// </summary>
        /// <param name="droneId">The drone to remove</param>
        public void RemoveDroneCharge(int droneId)
        {
            RemoveEntity(GetEntities<DroneCharge>().FirstOrDefault(droneCharge=>droneCharge.Droneld==droneId));
        }

        /// <summary>
        /// Get the time of start charging
        /// </summary>
        /// <param name="droneId">The drone in charging</param>
        public DateTime GetTimeStartOfCharge(int droneId)
        {
            return GetEntities<DroneCharge>().FirstOrDefault(drone => drone.Droneld == droneId).StartCharging;
        }
    }
}
