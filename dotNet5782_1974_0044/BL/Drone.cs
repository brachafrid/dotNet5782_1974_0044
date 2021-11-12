using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public DroneStatuses DroneStatuses { get; set; }
            public int BattaryMode { get; set; }
            public Location CurrentLocation { get; set; }
            public ParcellInTransfer Delivery { get; set; }
        }
    }

}
