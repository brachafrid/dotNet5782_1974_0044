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
                Drone = DroneWithParcelHandler.ConvertDroneWithParcel(parcel.Drone),
            };
        }
        public BO.Parcel ConvertBackParcel(Parcel parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Piority,
                DeliveryTime = parcel.DeliveryTime,
                AssignmentTime = parcel.AssignmentTime,
                CollectionTime = parcel.CollectionTime,
                CreationTime = parcel.CreationTime,
                CustomerReceives = CustomerInParcelHandler.ConvertBackCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = CustomerInParcelHandler.ConvertBackCustomerInParcel(parcel.CustomerSender),
                Drone = DroneWithParcelHandler.ConvertBackDroneWithParcel(parcel.Drone)
            };
        }
    }
}
