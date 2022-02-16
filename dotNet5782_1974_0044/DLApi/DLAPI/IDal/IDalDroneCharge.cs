using DO;
using System;
using System.Collections.Generic;

namespace DLApi
{
    public interface IDalDroneCharge
    {
        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        ///<param name="inTheStation">The predicate to screen out if the station id of the drone equal to a spesific station id </param>
        /// <returns>A list of DroneCarge</returns>
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation);
        /// <summary>
        /// Get drones charging
        /// </summary>
        /// <returns>drones charging</returns>
        public IEnumerable<DroneCharge> GetDronescharging();

        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        public void AddDroneCharge(int droneId, int stationId);

        /// <summary>
        /// Remove DroneCharge object from the list
        /// </summary>
        /// <param name="droneId">The drone to remove</param>
        public void RemoveDroneCharge(int droneId);

        /// <summary>
        /// Get the time of start charging
        /// </summary>
        /// <param name="droneId">The drone in charging</param>
        public DateTime GetTimeStartOfCharge(int droneId);

        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        ///<param name="inTheStation">The predicate to screen out if the station id of the drone equal to a spesific station id </param>
        /// <returns>A list of DroneCarge</returns>
        public int CountFullChargeSlots(int id);

        /// <summary>
        /// Return Electricity
        /// </summary>
        /// <returns>5 Electricity ratings </returns>
        (double, double, double, double, double) GetElectricity();
    }
}
