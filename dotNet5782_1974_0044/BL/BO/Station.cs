using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
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
