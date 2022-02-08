using System.Collections.Generic;




    namespace BO
    {
        public class Station
        {
            public int Id { get; init; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int AvailableChargingPorts { get; set; }
            public IEnumerable<DroneInCharging> DroneInChargings { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

