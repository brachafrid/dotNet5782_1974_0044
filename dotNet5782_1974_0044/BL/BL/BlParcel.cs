using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL
{
    public partial class BL : IblParcel
    {
        public void AddParcel(Parcel parcel)
        {
            if (ExistsIDTaxCheck(dal.GetParcels(), parcel.Id))
                throw new AnElementWithTheSameKeyAlreadyExistsInTheListException();
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcel.CustomerSender.Id))
                throw new KeyNotFoundException("sender not exist");
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcel.CustomerReceives.Id))
                throw new KeyNotFoundException("target not exist");
            dal.ParcelsReception(parcel.Id, parcel.CustomerSender.Id, parcel.CustomerReceives.Id, (IDAL.DO.WeightCategories)parcel.Weight, (IDAL.DO.Priorities)parcel.Priority);
        }
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone()
        {
           return dal.GetParcelsNotAssignedToDrone()
        }
        public Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BO.Parcel> GetParcels()
        {
            throw new NotImplementedException();
        }
        public void DeliveryParcelByDrone(int droneId)
        {
            throw new NotImplementedException();
        }
        public void AssingParcellToDrone(int droneId)
        {

        }
        private ParcelAtCustomer ParcelToParcelAtCustomer(Parcel parcel, string type)
        {
            ParcelAtCustomer newParcel = new ParcelAtCustomer();
            newParcel.Id = parcel.Id;
            newParcel.WeightCategory = parcel.Weight;
            newParcel.Priority = parcel.Priority;
            newParcel.DroneStatus = drones.Find(drone => drone.Id == parcel.Drone.Id).DroneStatus;
            if (type == "sender")
            {
                newParcel.Customer = new CustomerInParcel()
                {
                    Id = parcel.CustomerReceives.Id,
                    Name = parcel.CustomerReceives.Name
                };
            }
            else
            {
                newParcel.Customer = new CustomerInParcel()
                {
                    Id = parcel.CustomerSender.Id,
                    Name = parcel.CustomerSender.Name
                };
            }

            return newParcel;
        }

        public void AddParcel(IblParcel parcel)
        {
            throw new NotImplementedException();
        }
        private ParcelInTransfer CreateParcelInTransfer(int id)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(id);
            IDAL.DO.Customer sender = dal.GetCustomer(parcel.SenderId);
            IDAL.DO.Customer target = dal.GetCustomer(parcel.TargetId);
            return new ParcelInTransfer
            {
                Id = id,
                WeightCategory = (BO.WeightCategories)parcel.Weigth,
                Priority = (BO.Priorities)parcel.Priority,
                ParcelStatus = !parcel.PickedUp.Equals(default(DateTime)),
                CollectionPoint = new BO.Location() { Longitude = sender.Longitude, Latitude = sender.Latitude },
                DeliveryDestination = new BO.Location() { Longitude = target.Longitude, Latitude = target.Latitude },
                TransportDistance = Distance(new Location() { Longitude = sender.Longitude, Latitude = sender.Latitude }, new Location() { Longitude = sender.Longitude, Latitude = sender.Latitude }),
                CustomerSender = new CustomerInParcel() { Id = sender.Id, Name = sender.Name },
                CustomerReceives = new CustomerInParcel() { Id = target.Id, Name = target.Name }
            };
        }
        private Parcel mapParcel(IDAL.DO.Parcel parcel)
        {
            parcel.Sceduled
            Parcel p;
            p.
            return new Parcel()
            {
                
            }
        }

    }
}
