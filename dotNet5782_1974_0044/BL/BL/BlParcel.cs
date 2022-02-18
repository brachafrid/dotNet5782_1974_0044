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
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel parcelBl)
        {
            lock (dal)
            {
                if (!IsExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerSender.Id))
                    throw new KeyNotFoundException($"Sender Id: {parcelBl.CustomerSender.Id} not exist");
                if (!IsExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerReceives.Id))
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
                return dal.GetParcels().Select(Parcel =>MapParcel(Parcel));
        }


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
                lock (dal)
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

        #endregion


        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsNotActiveParcel(int id)
        {
            lock (dal)
               return dal.GetParcels().Any(parcel => parcel.Id == id && parcel.IsNotActive);
        }
    }
}