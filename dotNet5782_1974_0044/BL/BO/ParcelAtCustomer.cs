using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
      public  class ParcelAtCustomer
        {
            public int Id { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public Priorities priority { get; set; }
            public DroneStatuses status { get; set; }
            public CustomerInParcel Customer { get; set; }
        }
    }

}
