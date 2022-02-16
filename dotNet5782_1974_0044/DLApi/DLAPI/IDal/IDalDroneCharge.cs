using DO;
using System;
using System.Collections.Generic;

namespace DLApi
{
    public interface IDalDroneCharge
    {
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation);
        public IEnumerable<DroneCharge> GetDronescharging();
        public void AddDroneCharge(int droneId, int stationId);
        public void RemoveDroneCharge(int droneId);
        public DateTime GetTimeStartOfCharge(int droneId);
        /// <summary>
        /// Count full charge slots
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of the full charge slots</returns>
        public int CountFullChargeSlots(int id);

        /// <summary>
        /// Get Electricity
        /// </summary>
        /// <returns>5 Electricity ratings </returns>
        (double, double, double, double, double) GetElectricity();
    }
}
