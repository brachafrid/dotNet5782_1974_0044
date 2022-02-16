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

        /// <summary>
        /// Add new parcel
        /// </summary>
        /// <param name="SenderId">Sender Id</param>
        /// <param name="TargetId">Target Id</param>
        /// <param name="Weigth">Parcel Weigth</param>
        /// <param name="Priority">Parcel Priority</param>
        /// <param name="id">Parcel id</param>
        /// <param name="droneId">drone Id</param>
        /// <param name="requested">requested</param>
        /// <param name="sceduled">sceduled</param>
        /// <param name="pickedUp">pickedUp</param>
        /// <param name="delivered">delivered</param>
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = default, DateTime? sceduled = default, DateTime? pickedUp = default, DateTime? delivered = default);
        /// <summary>
        /// Get parcel
        /// </summary>
        /// <param name="id">parcel'sid</param>
        /// <returns>parcel</returns>
        public Parcel GetParcel(int id);
        public IEnumerable<Parcel> GetParcels();
        /// <summary>
        /// Get parcels not assigned to drone
        /// </summary>
        /// <param name="notAssign">(Predicate type of int: notAssign</param>
        /// <returns>parcels not assigned to drone</returns>
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign);
        /// <summary>
        /// Update parcel
        /// </summary>
        /// <param name="parcel">parcel</param>
        /// <param name="newParcel">new parcel</param>
        public void UpdateParcel(Parcel parcel,Parcel newParcel);

        /// <summary>
        /// Delete parcel
        /// </summary>
        /// <param name="id">parcel's id</param>
        public void DeleteParcel(int id);

    }
}
