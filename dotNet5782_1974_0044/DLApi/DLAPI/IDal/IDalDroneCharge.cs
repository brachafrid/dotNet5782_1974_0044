using DO;
using System;
using System.Collections.Generic;

namespace DLApi
{
    public interface IDalDroneCharge
    {
        /// <summary>
        /// Get drone charging in station
        /// </summary>
        /// <param name="inTheStation">Predicate type of int inTheStation</param>
        /// <returns>drone charging in station</returns>
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation);
        /// <summary>
        /// Get drones charging
        /// </summary>
        /// <returns>drones charging</returns>
        public IEnumerable<DroneCharge> GetDronescharging();
        /// <summary>
        /// Add drone charge
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="stationId">station's id</param>
        public void AddDroneCharge(int droneId, int stationId);

        /// <summary>
        /// Remove drone charge
        /// </summary>
        /// <param name="droneId">drone's id</param>
        public void RemoveDroneCharge(int droneId);
        /// <summary>
        /// Get start time of charging
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>start time of charging</returns>
        public DateTime GetTimeStartOfCharge(int droneId);

        /// <summary>
        /// Get Electricity
        /// </summary>
        /// <returns>5 Electricity ratings </returns>
        (double, double, double, double, double) GetElectricity();
    }
}
