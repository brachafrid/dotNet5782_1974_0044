using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : IBlParcel
    {
        #region Add
        /// <summary>
        /// Add a parcel to the list of parcels
        /// </summary>
        /// <param name="parcelBl">The parcel for Adding</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel parcelBl)
        {
            lock (dal)
            {
                if (!ExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerSender.Id))
                    throw new KeyNotFoundException($"Sender Id: {parcelBl.CustomerSender.Id} not exist");
                if (!ExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerReceives.Id))
                    throw new KeyNotFoundException($"Target id: { parcelBl.CustomerReceives.Id} not exist");
            }
            if (IsNotActiveCustomer(parcelBl.CustomerSender.Id))
                throw new DeletedExeption($"sender {parcelBl.CustomerSender.Id} deleted");
            if (IsNotActiveCustomer(parcelBl.CustomerReceives.Id))
                throw new DeletedExeption($"Reciver {parcelBl.CustomerReceives.Id} deleted");
            try
            {
                lock (dal)
                    dal.AddParcel(parcelBl.CustomerSender.Id, parcelBl.CustomerReceives.Id, (DO.WeightCategories)parcelBl.Weight, (DO.Priorities)parcelBl.Priority);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        #endregion

        #region Return
        /// <summary>
        /// Retrieves the list of parcels that not assigned to drone from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone(Predicate<int> notAssign)
        {
            try
            {
                lock (dal)
                    return dal.GetParcelsNotAssignedToDrone(notAssign).Select(parcel => MapParcelToList(parcel));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }

        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcels()
        {
            try
            {
                lock (dal)
                    return dal.GetParcels().Select(parcel => MapParcelToList(parcel));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }

        }

        /// <summary>
        /// Get active parcels
        /// </summary>
        /// <returns>list of active parcels</returns>
        // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetActiveParcels()
        {
            try
            {
                lock (dal)
                    return dal.GetParcels().Where(parcel => !parcel.IsNotActive).Select(parcel => MapParcelToList(parcel));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }


        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to BL parcel 
        /// </summaryparfcel
        /// <returns>A list of parcels to print</returns>
        private IEnumerable<Parcel> GetAllParcels()
        {
            lock (dal)
                return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }

        /// <summary>
        /// Retrieves the requested parcel from the data and converts it to BL parcel
        /// </summary>
        /// <param name="id">The requested parcel id</param>
        /// <param name="nullAble">enable null if the customer sender or reciver not found</param>
        /// <returns>A Bl parcel to print</returns>
        // [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            try
            {
                lock (dal)
                    return MapParcel(dal.GetParcel(id));
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        #endregion

        #region Update
        /// <summary>
        /// Assign the parcel to drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        /// <param name="droneId">The drone to assign</param>
        private void AssigningDroneToParcel(int parcelId, int droneId)
        {
            try
            {
                DO.Parcel parcel;
                lock (dal)
                    parcel = dal.GetParcel(parcelId);
                DO.Parcel newParcel = parcel;
                newParcel.DorneId = droneId;
                newParcel.Sceduled = DateTime.Now;
                dal.UpdateParcel(parcel, newParcel);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }

        }

        /// <summary>
        /// Collect the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelcollectionDrone(DO.Parcel parcel)
        {
            DO.Parcel newParcel = parcel;
            newParcel.PickedUp = DateTime.Now;
            lock (dal)
                dal.UpdateParcel(parcel, newParcel);
        }

        /// <summary>
        /// Deliverd the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelDeliveredDrone(DO.Parcel parcel)
        {
            DO.Parcel newParcel = parcel;
            newParcel.Delivered = DateTime.Now;
            lock (dal)
                dal.UpdateParcel(parcel, newParcel);

        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete parcel according to id
        /// </summary>
        /// <param name="id">id of parcel</param>
        // [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            try
            {
                Parcel parcel = GetParcel(id);
                if (parcel.Drone == null)
                    lock (dal)
                        dal.DeleteParcel(id);
                else
                {
                    DeleteParcelFromDrone(parcel.Drone.Id);
                    lock (dal)
                        dal.DeleteParcel(id);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Delete parcel from drone
        /// </summary>
        /// <param name="id">id of drone</param>
        private void DeleteParcelFromDrone(int id)
        {
            DroneToList drone = drones.FirstOrDefault(item => item.Id == id);
            if (drone != default)
            {
                drone.ParcelId = null;
                drone.DroneState = DroneState.AVAILABLE;
            }
        }
        #endregion

        /// <summary>
        /// Check if parcel is not active
        /// </summary>
        /// <param name="id">id of parcel</param>
        /// <returns>if parcel is not active</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsNotActiveParcel(int id)
        {
            lock (dal)
               return dal.GetParcels().Any(parcel => parcel.Id == id && parcel.IsNotActive);
        }

    }
}