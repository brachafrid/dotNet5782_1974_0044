using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Parcell
        {
            public int Id { get; set; }
            public Customer CustomerSender { get; set; }
            public Customer CustomerReceives { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public Drone Drone { get; set; }
            public DateTime CreationTime { get; set; }
            public DateTime AssignmentTime { get; set; }
            public DateTime CollectionTime { get; set; }
            public DateTime DeliveryTime { get; set; }

        }
    }

}
