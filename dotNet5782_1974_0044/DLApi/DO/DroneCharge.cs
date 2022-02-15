using System;

namespace DO
{
    public struct DroneCharge
    {
        public int Droneld { get; set; }
        public int Stationld { get; set; }
        public DateTime StartCharging { get; set; }
        public override string ToString()=>$"DroneCharge ID:{Droneld} Station:{Stationld}";
    }
}

