using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
      public class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel CustomerSender { get; set; }
            public CustomerInParcel CustomerReceives { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DroneWithParcel Drone { get; set; }
            public DateTime CreationTime { get; set; }
            public DateTime? AssignmentTime { get; set; }
            public DateTime? CollectionTime { get; set; }
            public DateTime? DeliveryTime { get; set; }

        }
    }

}
