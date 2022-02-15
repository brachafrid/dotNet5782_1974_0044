
using System.Collections.Generic;
using System;



namespace BLApi
{
    public interface IBlParcel
    {
        public void AddParcel(BO.Parcel parcel);
        public BO.Parcel GetParcel(int id);
        public IEnumerable<BO.ParcelToList> GetParcels();
        public IEnumerable<BO.ParcelToList> GetActiveParcels();
        public IEnumerable<BO.ParcelToList> GetParcelsNotAssignedToDrone(Predicate<int> notAssign);
        public void DeleteParcel(int id);
        public bool IsNotActiveParcel(int id);

    }
}


