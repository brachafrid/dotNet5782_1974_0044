using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IblParcel
    {
        public void AddParcel(Parcel parcel)
        {
            parcel.
        }
        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, BO.WeightCategories Weight, BO.Priorities priority)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone()
        {
            throw new NotImplementedException();
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
    }
}
