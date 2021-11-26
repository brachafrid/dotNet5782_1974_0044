using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IBlParcel
    {
        public void AddParcel(BO.Parcel parcel);
        public BO.Parcel GetParcel(int id);
        public IEnumerable<BO.ParcelToList> GetParcels();
        public IEnumerable<BO.ParcelToList> GetParcelsNotAssignedToDrone();
    }
}
