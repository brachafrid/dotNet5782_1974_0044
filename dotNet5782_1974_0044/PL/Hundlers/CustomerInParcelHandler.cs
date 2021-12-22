using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Hundlers
{
    class CustomerInParcelHandler
    {
        public static BO.CustomerInParcel ConvertCustomerInParcel(PO.CustomerInParcel customerInParcel)
        {
            return new BO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }

        public static PO.CustomerInParcel ConvertBackCustomerInParcel(BO.CustomerInParcel customerInParcel)
        {
            return new PO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }
    }
}
