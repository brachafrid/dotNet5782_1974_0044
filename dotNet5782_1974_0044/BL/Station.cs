using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int AvailableChargeSlot { get; set; }
            public List<DroneInCharging> DroneInChargings { get; set; }
        }
    }

}
