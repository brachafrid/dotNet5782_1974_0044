using System;
using System.Collections.Generic;
using System.Linq;

using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        //--------------------------------------Adding---------------------------
        /// <summary>
        /// Gets parameters and create new parcel 
        /// </summary>
        /// <param name="SenderId"> Id of sener</param>
        /// <param name="TargetId"> Id of target</param>
        /// <param name="Weigth"> The weigth of parcel (light- 0,medium - 1,heavy - 2)</param>
        /// <param name="Priority"> The priority of send the parcel (regular - 0,fast - 1,emergency - 2)</param>
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime requested = default, DateTime sceduled = default, DateTime pickedUp = default, DateTime delivered = default)
        {
            if (!ExistsIDTaxCheck(GetCustomers(), SenderId))
                throw new KeyNotFoundException("Sender not exist");
            if (!ExistsIDTaxCheck(GetCustomers(), TargetId))
                throw new KeyNotFoundException("Target not exist");
            Parcel newParcel = new();
            newParcel.Id = id==0?++DataSorce.Config.IdParcel:id;
            newParcel.SenderId = SenderId;
            newParcel.TargetId = TargetId;
            newParcel.Weigth = Weigth;
            newParcel.Priority = Priority;
            newParcel.Requested =requested==default? DateTime.Now:requested;
            newParcel.Sceduled =sceduled;
            newParcel.PickedUp = pickedUp;
            newParcel.Delivered = delivered;
            newParcel.DorneId = droneId;
            DataSorce.Parcels.Add(newParcel);
        }

        //-----------------------------------------------------Display--------------------------------------
        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        public Parcel GetParcel(int id)
        {
           Parcel parcel = DataSorce.Parcels.FirstOrDefault(item => item.Id == id);
            if (parcel.Equals(default(Parcel)))
                throw new KeyNotFoundException("There is not suitable parcel in data");
            return parcel;
        }

        /// <summary>
        /// Prepares the list of Parcels for display
        /// </summary>
        /// <returns>A list of parcel</returns>
        public IEnumerable<Parcel> GetParcels() => DataSorce.Parcels;

        /// <summary>
        /// Find the Parcels that not assign to drone
        /// </summary>
        /// <returns>A list of the requested Parcels</returns>
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone() => DataSorce.Parcels.FindAll(item => item.DorneId == 0);
        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a Parcel from the list
        /// </summary>
        /// <param name="station"></param>
        public void RemoveParcel(Parcel parcel)
        {
            DataSorce.Parcels.Remove(parcel);
        }
    }
}
