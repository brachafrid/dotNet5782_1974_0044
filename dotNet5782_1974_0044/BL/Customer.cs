using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Location Location { get; set; }
            public List<DeliveryToACustomer> FromCustomer  { get; set; }
            public List<DeliveryToACustomer> ToCustomer  { get; set; }
        }
    }

}
