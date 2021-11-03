using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public partial class BL : IBL.IBL
    {
        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, IBL.BO.WeightCategories Weight, IBL.BO.Priorities priority)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IDAL.DO.Parcel> GetParcelsNotAssignedToDrone()
        {
            throw new NotImplementedException();
        }
        public IDAL.DO.Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IDAL.DO.Parcel> GetParcels()
        {
            throw new NotImplementedException();
        }
        public void DeliveryParcelByDrone(int droneId)
        {
            throw new NotImplementedException();
        }
    }
}
