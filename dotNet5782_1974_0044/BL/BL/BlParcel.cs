using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace IBL
{
    public partial class BL : IblParcel
    {
        //-----------------------------------------------------------Adding------------------------------------------------------------------------
        /// <summary>
        /// Add a parcel to the list of parcels
        /// </summary>
        /// <param name="parcelBl">The parcel for Adding</param>
        public void AddParcel(Parcel parcelBl)
        {
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerSender.Id))
                throw new KeyNotFoundException("sender not exist");
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerReceives.Id))
                throw new KeyNotFoundException("target not exist");
            dal.ParcelsReception(parcelBl.CustomerSender.Id, parcelBl.CustomerReceives.Id, (IDAL.DO.WeightCategories)parcelBl.Weight, (IDAL.DO.Priorities)parcelBl.Priority);
        }

        //-------------------------------------------------Return List-----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of parcels that not assigned to drone from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
        public IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone()
        {
            return dal.GetParcelsNotAssignedToDrone().Select(parcel => mapParcelToList(parcel));
        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
        public IEnumerable<ParcelToList> GetParcels()
        {
            return dal.GetParcels().Select(parcel => mapParcelToList(parcel));
        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to BL parcel 
        /// </summaryparfcel
        /// <returns>A list of parcels to print</returns>
        private IEnumerable<Parcel> getAllParcels()
        {
            return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }

        //--------------------------------------------------Return-----------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested parcel from the data and converts it to BL parcel
        /// </summary>
        /// <param name="id">The requested parcel id</param>
        /// <returns>A Bl parcel to print</returns>
        public Parcel GetParcel(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetParcels(), id))
                throw new KeyNotFoundException();
            return mapParcel(dal.GetParcel(id));
        }

        //-------------------------------------------------Updating--------------------------------------------------------------------------------------
        /// <summary>
        /// Assign the parcel to drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        /// <param name="droneId">The drone to assign</param>
        private void AssigningDroneToParcel(int parcelId, int droneId)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.DorneId = droneId;
            parcel.Sceduled = DateTime.Now;
            dal.ParcelsReception(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id);
        }

        /// <summary>
        /// Collect the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelcollectionDrone(int parcelId)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.PickedUp = DateTime.Now;
            dal.ParcelsReception(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id);
        }

        /// <summary>
        /// Deliverd the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelDeliveredDrone(int parcelId)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.Delivered = DateTime.Now;
            dal.ParcelsReception(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id);
        }

        //-----------------------------------------------Help function-----------------------------------------------------------------------------------
        /// <summary>
        /// Convert a DAL parcel to BL parcel
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <returns>The converted parcel</returns>
        private Parcel mapParcel(IDAL.DO.Parcel parcel)
        {
            var tmpDrone = drones.FirstOrDefault(drone => drone.Id == parcel.DorneId);
            if (tmpDrone.Equals(default))
                throw new KeyNotFoundException();
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
                Drone = mapDroneWithParcel(tmpDrone)
            };
        }

    }
}