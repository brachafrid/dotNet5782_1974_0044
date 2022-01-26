﻿using System;
using System.Collections.Generic;
using System.Linq;

using DO;

namespace Dal
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
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = default, DateTime? sceduled = default, DateTime? pickedUp = default, DateTime? delivered = default)
        {
            if (!ExistsIDTaxCheckNotDelited(GetCustomers(), SenderId))
                throw new KeyNotFoundException("Sender not exist");
            if (!ExistsIDTaxCheckNotDelited(GetCustomers(), TargetId))
                throw new KeyNotFoundException("Target not exist");
            Parcel newParcel = new();
            newParcel.Id = id == 0 ? ++DataSorce.Config.IdParcel : id;
            newParcel.SenderId = SenderId;
            newParcel.TargetId = TargetId;
            newParcel.Weigth = Weigth;
            newParcel.Priority = Priority;
            newParcel.Requested = requested == null ? DateTime.Now : requested;
            newParcel.Sceduled = sceduled;
            newParcel.PickedUp = pickedUp;
            newParcel.Delivered = delivered;
            newParcel.DorneId = droneId;
            AddEntity(newParcel);
        }

        //-----------------------------------------------------Display--------------------------------------
        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        public Parcel GetParcel(int id)
        {
            Parcel parcel =getEntities<Parcel>().FirstOrDefault(item => item.Id == id);
            if (parcel.Equals(default(Parcel)) || parcel.IsNotActive )
                throw new KeyNotFoundException("There is not suitable parcel in data");
            return parcel;
        }

        /// <summary>
        /// Prepares the list of Parcels for display
        /// </summary>
        /// <returns>A list of parcel</returns>
        public IEnumerable<Parcel> GetParcels() => DataSorce.Parcels.Where(p => !p.IsNotActive);

        /// <summary>
        /// Find the Parcels that not assign to drone
        /// </summary>
        /// <param name="notAssign">The predicate to screen out if the parcel not assign to drone</param>
        /// <returns>A list of the requested Parcels</returns>
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign) => getEntities<Parcel>().Where(item =>notAssign(item.DorneId));
        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a Parcel from the list
        /// </summary>
        /// <param name="station"></param>
        public void RemoveParcel(Parcel parcel)
        {
            RemoveEntity(parcel);
        }

        public void DeleteParcel(int id)
        {
            Parcel parcel = getEntities<Parcel>().FirstOrDefault(item => item.Id == id);
            RemoveEntity(parcel);
            parcel.IsNotActive = true;
            AddEntity(parcel);
        }
    }
}
