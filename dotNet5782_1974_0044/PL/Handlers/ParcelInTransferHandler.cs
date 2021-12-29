using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;

namespace PL
{
    public static class ParcelInTransferHandler
    {
        public static ParcelInTransfer ConvertParcelInTransfer(BO.ParcelInTransfer parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Piority = (Priorities)parcel.Priority,
                ParcelState = parcel.ParcelState,
                CollectionPoint = LocationHandler.ConvertLocation(parcel.CollectionPoint),
                CustomerReceives = CustomerInParcelHandler.ConvertCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = CustomerInParcelHandler.ConvertCustomerInParcel(parcel.CustomerSender),
                DeliveryDestination = LocationHandler.ConvertLocation(parcel.DeliveryDestination),
                TransportDistance = parcel.TransportDistance,
                Weight = (WeightCategories)parcel.WeightCategory
            };
        }
        public static BO.ParcelInTransfer ConvertBackParcelInTransfer(ParcelInTransfer parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Priority = (BO.Priorities)parcel.Piority,
                ParcelState = parcel.ParcelState,
                CollectionPoint = LocationHandler.ConvertBackLocation(parcel.CollectionPoint),
                CustomerReceives = CustomerInParcelHandler.ConvertBackCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = CustomerInParcelHandler.ConvertBackCustomerInParcel(parcel.CustomerSender),
                DeliveryDestination = LocationHandler.ConvertBackLocation(parcel.DeliveryDestination),
                TransportDistance = parcel.TransportDistance,
                WeightCategory = (BO.WeightCategories)parcel.Weight
            };
        }
    }
}
