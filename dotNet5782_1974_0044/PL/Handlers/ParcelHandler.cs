using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using PL.PO;

namespace PL
{
    public class ParcelHandler
    {
        private IBL ibal = BLFactory.GetBL();
        public Parcel ConvertParcel(BO.Parcel parcel)
        {
            return new Parcel()
            {
                Id = parcel.Id,
                Weight = (WeightCategories)parcel.Weight,
                Piority = (Priorities)parcel.Priority,
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
        ParcelToList ConvertParcelToList(BO.ParcelToList parcel)
        {
            return new()
            {
                Id = parcel.Id,
                PackageMode =(PackageModes) parcel.PackageMode,
                Piority = (Priorities)parcel.Piority,
                Weight = (WeightCategories)parcel.Weight,
                CustomerReceives = new CustomerHandler().ConvertCustomer(parcel.CustomerReceives),
                CustomerSender = new CustomerHandler().ConvertCustomer(parcel.CustomerSender)
            };
        }
        public void AddParcel(ParcelAdd parcel)
        {
            ibal.AddParcel(ConvertBackParcelAdd(parcel));
        }

        public void AssigningDroneToParcel(Drone drone)
        {
            ibal.AssingParcelToDrone(drone.Id);
        }

        public void ParcelcollectionDrone(Drone drone)
        {
            ibal.ParcelCollectionByDrone(drone.Id);
        }

        public void ParcelDeliveredDrone(Drone drone)
        {
            ibal.DeliveryParcelByDrone(drone.Id);
        }

        //internal void UpdateParcel(int id, object name, object phone)
        //{
        //    ibal.AssingParcelToDrone(id);
        //    ibal.AssingParcelToDrone(id);
        //    throw new NotImplementedException();
        //}

        public Parcel GetParcel(int id) => ConvertParcel(ibal.GetParcel(id));
        public IEnumerable<ParcelToList> GetParcels => ibal.GetParcels().Select(parcel => ConvertParcelToList(parcel));
        public IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone=>ibal.GetParcelsNotAssignedToDrone((int num)=> num == 0).Select(parcel => ConvertParcelToList(parcel));
        public BO.Parcel ConvertBackParcelAdd(ParcelAdd parcel)
        {
            return new()
            {
                Priority = (BO.Priorities)parcel.Piority,
                Weight = (BO.WeightCategories)parcel.Weight,
                CustomerReceives = CustomerInParcelHandler.ConvertBackCustomerInParcel(new() { Id = parcel.CustomerReceives}),
                CustomerSender = CustomerInParcelHandler.ConvertBackCustomerInParcel(new() { Id = parcel.CustomerSender })
            };
        }
    }
}
