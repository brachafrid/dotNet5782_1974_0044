using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IblParcel
    {
        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, BO.WeightCategories Weight, BO.Priorities priority);
        public void AssingParcellToDrone(int droneId);
        public void ParcelCollectionByDrone(int DroneId);
        public void DeliveryParcelByDrone(int droneId);
        public BO.Parcel GetParcel(int id);
        public IEnumerable<IDAL.DO.Parcel> GetParcels();
        public IEnumerable<IDAL.DO.Parcel> GetParcelsNotAssignedToDrone();
    }
}
