using System;

namespace DO
{
    public struct DroneCharge
    {
        /// <summary>
        /// The id of the drone in charging 
        /// </summary>
        public int Droneld { get; set; }
        /// <summary>
        /// The id of station that the drone in charge there
        /// </summary>
        public int Stationld { get; set; }
        /// <summary>
        /// The time of start the charging
        /// </summary>
        public DateTime StartCharging { get; set; }
        public override string ToString()
        {
            return $"DroneCharge ID:{Droneld} Station:{Stationld}";
        }
    }
}

