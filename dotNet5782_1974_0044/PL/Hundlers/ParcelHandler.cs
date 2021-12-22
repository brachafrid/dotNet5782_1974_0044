using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using PL.PO;

namespace PL.Hundlers
{
    public class ParcelHandler
    {
        public Parcel ConvertParcel(BO.Parcel parcel)
        {
            return new Parcel()
            {
                Id = parcel.Id,
                Weight = (PO.WeightCategories)parcel.Weight,
                Piority = (PO.Priorities)parcel.Priority,
                DeliveryTime = parcel.DeliveryTime,
                AssignmentTime = parcel.AssignmentTime,
                CollectionTime = parcel.CollectionTime,
                CreationTime = parcel.CreationTime,
                CustomerReceives = CustomerInParcelHandler.ConvertCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = CustomerInParcelHandler.ConvertCustomerInParcel(parcel.CustomerSender),
                Drone = 
            }
        }
    }
}
