using System;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLApi
{
    public interface IDalParcel
    {
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = default, DateTime? sceduled = default, DateTime? pickedUp = default, DateTime? delivered = default);
        public Parcel GetParcel(int id);
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign);
        public void RemoveParcel(Parcel parcel);
        public void DeleteParcel(int id);

    }
}
