using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalXML
{
   partial class DalXML
    {
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = null, DateTime? sceduled = null, DateTime? pickedUp = null, DateTime? delivered = null)
        {
            throw new NotImplementedException();
        }
        public void DeleteParcel(int id)
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

        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign)
        {
            throw new NotImplementedException();
        }
        public void RemoveParcel(Parcel parcel)
        {
            throw new NotImplementedException();
        }
    }
}
