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
                throw new KeyNotFoundException("Add parcel -BL_:Sender not exist");
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcelBl.CustomerReceives.Id))
                throw new KeyNotFoundException("Add parcel -BL-:Target not exist");
            try
            {
                dal.AddParcel(parcelBl.CustomerSender.Id, parcelBl.CustomerReceives.Id, (IDAL.DO.WeightCategories)parcelBl.Weight, (IDAL.DO.Priorities)parcelBl.Priority);
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {

                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
           
        }

        //-------------------------------------------------Return List-----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of parcels that not assigned to drone from the data and converts it to parcel to list
        /// </summary>
        /// <returns>A list of parcels to print</returns>
        public IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone()
        {
            return dal.GetParcelsNotAssignedToDrone().Select(parcel => mapParcelToList(parcel)); ;
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
            try
            {
                return mapParcel(dal.GetParcel(id));
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
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
                IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
                dal.RemoveParcel(parcel);
                parcel.DorneId = droneId;
                parcel.Sceduled = DateTime.Now;
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id,parcel.DorneId,parcel.Requested,parcel.Sceduled,parcel.PickedUp,parcel.Delivered);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Assigning Drone To Parcel -BL-"+ex.Message);
            }
            catch(IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Assigning Drone To Parcel -BL-"+ex.Message );
            }

        }

        /// <summary>
        /// Collect the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelcollectionDrone(int parcelId)
        {
            try
            {
                IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
                dal.RemoveParcel(parcel);
                parcel.PickedUp = DateTime.Now;
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id,parcel.DorneId, parcel.Requested, parcel.Sceduled, parcel.PickedUp, parcel.Delivered);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Parcel ollection Drone -BL-"+ex.Message );
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Parcel ollection Drone -BL-"+ex.Message );
            }

        }

        /// <summary>
        /// Deliverd the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void ParcelDeliveredDrone(int parcelId)
        {
            try
            {
                IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
                dal.RemoveParcel(parcel);
                parcel.Delivered = DateTime.Now;
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id,parcel.DorneId, parcel.Requested, parcel.Sceduled, parcel.PickedUp, parcel.Delivered);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message + "Parcel Delivered Drone -BL-");
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message + "Parcel Delivered Drone -BL-");
            }
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

        

    }
}