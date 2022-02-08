using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public partial class BL : IBlParcel
    {
        //-----------------------------------------------------------Adding------------------------------------------------------------------------
        /// <summary>
        /// Add a parcel to the list of parcels
        /// </summary>
        /// <param name="parcelBl">The parcel for Adding</param>
        public void AddParcel(Parcel parcelBl)
        {
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerSender.Id))
                throw new KeyNotFoundException($"Sender Id: {parcelBl.CustomerSender.Id} not exist");
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerReceives.Id))
                throw new KeyNotFoundException($"Target id: { parcelBl.CustomerReceives.Id} not exist");
            if (IsNotActiveCustomer(parcelBl.CustomerSender.Id))
                throw new DeletedExeption($"sender {parcelBl.CustomerSender.Id} deleted");
            if (IsNotActiveCustomer(parcelBl.CustomerReceives.Id))
                throw new DeletedExeption($"Reciver {parcelBl.CustomerReceives.Id} deleted");
            try
            {
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
            //catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            //{
            //    throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            //}
        }

        //-------------------------------------------------Return List-----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of parcels that not assigned to drone from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
        public IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone(Predicate<int> notAssign)
        {
            try
            {
                return dal.GetParcelsNotAssignedToDrone(notAssign).Select(parcel => MapParcelToList(parcel));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
        public IEnumerable<ParcelToList> GetParcels()
        {
            try
            {
                return dal.GetParcels().Select(parcel => MapParcelToList(parcel));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        public IEnumerable<ParcelToList> GetActiveParcels()
        {
            try
            {
                return dal.GetParcels().Where(parcel => !parcel.IsNotActive).Select(parcel => MapParcelToList(parcel));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to BL parcel 
        /// </summaryparfcel
        /// <returns>A list of parcels to print</returns>
        private IEnumerable<Parcel> GetAllParcels()
        {
            return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }

        //--------------------------------------------------Return-----------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested parcel from the data and converts it to BL parcel
        /// </summary>
        /// <param name="id">The requested parcel id</param>
        /// <param name="nullAble">enable null if the customer sender or reciver not found</param>
        /// <returns>A Bl parcel to print</returns>
        public Parcel GetParcel(int id)
        {
            try
            {
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

        //-------------------------------------------------Updating--------------------------------------------------------------------------------------
        /// <summary>
        /// Assign the parcel to drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        /// <param name="droneId">The drone to assign</param>
        private void AssigningDroneToParcel(int parcelId, int droneId)
        {
            try
            {
                DO.Parcel parcel = dal.GetParcel(parcelId);
                DO.Parcel newParcel = parcel;
                newParcel.DorneId = droneId;
                newParcel.Sceduled = DateTime.Now;
                dal.UpdateParcel(parcel, newParcel);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            //catch(DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            //{
            //    throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message );
            //}

        }

        /// <summary>
        /// Collect the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelcollectionDrone(int parcelId)
        {
            try
            {
                DO.Parcel parcel = dal.GetParcel(parcelId);
                DO.Parcel newParcel = parcel;
                newParcel.PickedUp = DateTime.Now;
                dal.UpdateParcel(parcel, newParcel);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }

            //catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            //{
            //    throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message );
            //}

        }

        /// <summary>
        /// Deliverd the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelDeliveredDrone(int parcelId)
        {
            DO.Parcel parcel;
            try
            {
                parcel = dal.GetParcel(parcelId);
                DO.Parcel newParcel = parcel;
                newParcel.Delivered = DateTime.Now;
                dal.UpdateParcel(parcel, newParcel);

            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            //catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            //{
            //    throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            //}

        }

        public void DeleteParcel(int id)
        {
            try
            {
                Parcel parcel = GetParcel(id);
                if (parcel.Drone == null)
                {
                    dal.DeleteParcel(id);
                }
                else
                {
                    DeleteParcelFromDrone(parcel.Drone.Id);
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
        public bool IsNotActiveParcel(int id) => dal.GetParcels().Any(parcel => parcel.Id == id && parcel.IsNotActive);

        //-----------------------------------------------Help function-----------------------------------------------------------------------------------
        /// <summary>
        /// Convert a DAL parcel to BL parcel
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <param name="nullAble"></param>
        /// <returns>The converted parcel</returns>
        private Parcel MapParcel(DO.Parcel parcel)
        {
            var tmpDrone = drones.FirstOrDefault(drone => drone.Id == parcel.DorneId);
            return new Parcel()
            {
                Id = parcel.Id,
                CustomerReceives = MapCustomerInParcel(dal.GetCustomer(parcel.TargetId)),
                CustomerSender = MapCustomerInParcel(dal.GetCustomer(parcel.SenderId)),
                Weight = (BO.WeightCategories)parcel.Weigth,
                Priority = (BO.Priorities)parcel.Priority,
                AssignmentTime = parcel.Sceduled,
                CollectionTime = parcel.PickedUp,
                CreationTime = parcel.Requested,
                DeliveryTime = parcel.Delivered,
                Drone = tmpDrone != default ? MapDroneWithParcel(tmpDrone) : null
            };
        }

        private void DeleteParcelFromDrone(int id)
        {
            DroneToList drone = drones.FirstOrDefault(item => item.Id == id);
            drones.Remove(drone);
            drone.ParcelId = null;
            drone.DroneState = DroneState.AVAILABLE;
            drones.Add(drone);
        }

    }
}