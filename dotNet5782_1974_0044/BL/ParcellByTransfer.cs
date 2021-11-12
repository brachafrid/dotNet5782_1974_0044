using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using IBL.BO;

namespace IBL
{
    namespace BO
    {
        class ParcellByTransfer
        {
            public int Id { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public Priority priority { get; set; }
            public bool DeliveryStatus { get; set; }
            public Location CollectionPoint { get; set; }
            public Location DeliveryDestination { get; set; }
            public double TransportDistance { get; set; }

        }
    }

}
