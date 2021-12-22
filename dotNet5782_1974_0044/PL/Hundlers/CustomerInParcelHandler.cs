using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Hundlers
{
   public static class CustomerInParcelHandler
   {
        public static PO.CustomerInParcel ConvertCustomerInParcel(BO.CustomerInParcel customerInParcel)
        {
            return new PO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }
        public static BO.CustomerInParcel ConvertBackCustomerInParcel(PO.CustomerInParcel customerInParcel)
        {
            return new BO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }

     
    }
}
