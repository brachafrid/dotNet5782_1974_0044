using System.Collections.Generic;



namespace BL
{
    namespace BO
    {
        public class Station
        {
            public int Id { get; init; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int AvailableChargingPorts { get; set; }
            public List<DroneInCharging> DroneInChargings { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

}
