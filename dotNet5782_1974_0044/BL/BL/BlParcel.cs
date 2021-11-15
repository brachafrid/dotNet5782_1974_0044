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
        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, IBL.BO.WeightCategories Weight, IBL.BO.Priorities priority)
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
        public IEnumerable<Parcel> GetParcels()
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
        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, WeightCategories Weight, Priorities priority)
        {
            throw new NotImplementedException();
        }
        private ParcelAtCustomer ParcelToParcelAtCustomer(Parcel parcel)
        {
            return new ParcelAtCustomer()
            {
                Id = parcel.Id,
                WeightCategory = parcel.Weight,
                Priority = parcel.Priority,
                DroneStatus = .DroneStatuses,
                Customer = new CustomerInParcel()
                {
                    Id =
                    
                }


            }
        }

        public void AddParcel(IblParcel parcel)
        {
            throw new NotImplementedException();
        }
    }
}
