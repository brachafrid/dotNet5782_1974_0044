using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
       public class Drone
        {
            public int Id { get; init; }
            public string Model { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public DroneStatuses DroneState { get; set; }
            public double BattaryMode { get; set; }
            public Location CurrentLocation { get; set; }
            public ParcelInTransfer Parcel { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

}
