using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int Id { get; set; }
            public string DroneModel { get; set; }
            public WeightCategories Weight { get; set; }
            public double BatteryStatus { get; set; }
            public DroneStatuses DroneStatus { get; set; }
            public Location CurrentLocation { get; set; }
            public int? ParcelId { get; set; }
        }
    }

}
