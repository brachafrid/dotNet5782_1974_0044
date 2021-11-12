using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DeliveryToACustomer
        {
            public int Id { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public Priority priority { get; set; }
            public DroneStatuses status { get; set; }
            public CustomerInParcel Customer { get; set; }
        }
    }

}
