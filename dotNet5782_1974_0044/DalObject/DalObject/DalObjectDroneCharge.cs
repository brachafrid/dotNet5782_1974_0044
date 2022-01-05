
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
        public int CountFullChargeSlots(int id)
        {
            int count = 0;
            foreach (DroneCharge item in DataSorce.DroneCharges)
            {
                if (item.Stationld == id)
                    ++count;
            }
            return count;
        }

        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        ///<param name="inTheStation">The predicate to screen out if the station id of the drone equal to a spesific station id </param>
        /// <returns>A list of DroneCarge</returns>
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation)
        {
            return DataSorce.DroneCharges.FindAll(item => inTheStation(item.Stationld)).Select(item => item.Droneld);
        }
        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        public void AddDRoneCharge(int droneId, int stationId)
        {
            DataSorce.DroneCharges.Add(new DroneCharge() { Droneld = droneId, Stationld = stationId, StartCharging = DateTime.Now });
        }
        /// <summary>
        /// Remove DroneCharge object from the list
        /// </summary>
        /// <param name="droneId">The drone to remove</param>
        public void RemoveDroneCharge(int droneId)
        {
            DataSorce.DroneCharges.RemoveAll(drone=>drone.Droneld==droneId);
        }

        /// <summary>
        /// Get the time of start charging
        /// </summary>
        /// <param name="droneId">The drone in charging</param>
        public DateTime GetTimeStartOfCharge(int droneId)
        {
            return DataSorce.DroneCharges.FirstOrDefault(drone => drone.Droneld == droneId).StartCharging;
        }
    }
}
