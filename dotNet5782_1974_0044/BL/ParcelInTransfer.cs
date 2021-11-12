using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelInTransfer
        {
            public int Id { get; set; }
            public Priority priority { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Receives { get; set; }

        }
    }

}
