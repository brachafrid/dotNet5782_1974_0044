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
      public class ParcelInTransfer
        {
            public int Id { get; init; }
            public WeightCategories WeightCategory { get; set; }
            public Priorities Priority { get; set; }
            public bool ParcelStatus { get; set; }
            public Location CollectionPoint { get; set; }
            public Location DeliveryDestination { get; set; }
            public double TransportDistance { get; set; }
            public CustomerInParcel CustomerSender { get; set; }
            public CustomerInParcel CustomerReceives { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }

        }
    }

}
