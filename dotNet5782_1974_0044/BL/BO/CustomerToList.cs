using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    namespace BO
    {
        class CustomerToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int NumParcellSentDelivered { get; set; }
            public int NumParcellSentNotDelivered { get; set; }
            public int NumParcellReceived { get; set; }
            public int NumParcellWayToCustomer { get; set; }
        }
    }
}
