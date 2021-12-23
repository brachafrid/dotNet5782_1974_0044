using BLApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class ParcelAtCustomerHandler
    {
        public static BO.ParcelAtCustomer ConvertBackParcelAtCustomer(PO.ParcelAtCustomer parcelAtCustomer)
        {
            return new BO.ParcelAtCustomer()
            {
                Id = parcelAtCustomer.Id,
                WeightCategory = (BO.WeightCategories)parcelAtCustomer.Weight,
                Priority = (BO.Priorities)parcelAtCustomer.Piority,
                State = (BO.PackageModes)parcelAtCustomer.PackageMode,
                Customer = CustomerInParcelHandler.ConvertBackCustomerInParcel(parcelAtCustomer.Customer)
            };
        }

        public static PO.ParcelAtCustomer ConvertParcelAtCustomer(BO.ParcelAtCustomer parcelAtCustomer)
        {
            return new PO.ParcelAtCustomer()
            {
                Id = parcelAtCustomer.Id,
                Weight = (PO.WeightCategories)parcelAtCustomer.WeightCategory,
                Piority = (PO.Priorities)parcelAtCustomer.Priority,
                PackageMode = (PO.PackageModes)parcelAtCustomer.State,
                Customer = CustomerInParcelHandler.ConvertCustomerInParcel(parcelAtCustomer.Customer)
            };
        }
    }
}
