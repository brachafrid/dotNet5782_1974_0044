
using System.Collections.Generic;
using System;



namespace BLApi
{
    public interface IBlParcel
    {
        /// <summary>
        /// Add a parcel to the list of parcels
        /// </summary>
        /// <param name="parcelBl">The parcel for Adding</param>
        public void AddParcel(BO.Parcel parcel);

        /// <summary>
        /// Retrieves the requested parcel from the data and converts it to BL parcel
        /// </summary>
        /// <param name="id">The requested parcel id</param>
        /// <param name="nullAble">enable null if the customer sender or reciver not found</param>
        /// <returns>A Bl parcel to print</returns>
        public BO.Parcel GetParcel(int id);

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
        public IEnumerable<BO.ParcelToList> GetParcels();

        /// <summary>
        /// Get active parcels
        /// </summary>
        /// <returns>list of active parcels</returns>
        public IEnumerable<BO.ParcelToList> GetActiveParcels();

        /// <summary>
        /// Retrieves the list of parcels that not assigned to drone from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
        public IEnumerable<BO.ParcelToList> GetParcelsNotAssignedToDrone(Predicate<int> notAssign);

        /// <summary>
        /// Delete parcel according to id
        /// </summary>
        /// <param name="id">id of parcel</param>
        public void DeleteParcel(int id);

        /// <summary>
        /// Check if parcel is not active
        /// </summary>
        /// <param name="id">id of parcel</param>
        /// <returns>if parcel is not active</returns>
        public bool IsNotActiveParcel(int id);

    }
}


