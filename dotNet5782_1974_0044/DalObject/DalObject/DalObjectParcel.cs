using System;
using System.Collections.Generic;
using System.Linq;
using DLApi;
using System.Runtime.CompilerServices;
using DO;

namespace Dal
{
    public partial class DalObject:IDalParcel
    {
        //--------------------------------------Adding---------------------------
        /// <summary>
        /// Gets parameters and create new parcel 
        /// </summary>
        /// <param name="SenderId"> Id of sener</param>
        /// <param name="TargetId"> Id of target</param>
        /// <param name="Weigth"> The weigth of parcel (light- 0,medium - 1,heavy - 2)</param>
        /// <param name="Priority"> The priority of send the parcel (regular - 0,fast - 1,emergency - 2)</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = default, DateTime? sceduled = default, DateTime? pickedUp = default, DateTime? delivered = default)
        {
            if (!ExistsIDTaxCheckNotDelited(GetCustomers(), SenderId))
                throw new KeyNotFoundException($"Sender Id: {SenderId} not exist");
            if (!ExistsIDTaxCheckNotDelited(GetCustomers(), TargetId))
                throw new KeyNotFoundException($"Target id: {TargetId} not exist");
            Parcel newParcel = new()
            {
                Id = id == 0 ? ++DataSorce.Config.IdParcel : id,
                SenderId = SenderId,
                TargetId = TargetId,
                Weigth = Weigth,
                Priority = Priority,
                Requested = requested == null ? DateTime.Now : requested,
                Sceduled = sceduled,
                PickedUp = pickedUp,
                Delivered = delivered,
                DorneId = droneId,
            };
            DalObjectService.AddEntity(newParcel);
        }

        //-----------------------------------------------------Display--------------------------------------
        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            Parcel parcel =DalObjectService.GetEntities<Parcel>().FirstOrDefault(item => item.Id == id);
            if (parcel.Equals(default(Parcel)) )
                throw new KeyNotFoundException($"There is not suitable parcel in data , the wanted parcel is: {id}");
            return parcel;
        }

        /// <summary>
        /// Prepares the list of Parcels for display
        /// </summary>
        /// <returns>A list of parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels() =>DalObjectService.GetEntities<Parcel>();

        /// <summary>
        /// Find the Parcels that not assign to drone
        /// </summary>
        /// <param name="notAssign">The predicate to screen out if the parcel not assign to drone</param>
        /// <returns>A list of the requested Parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign) => DalObjectService.GetEntities<Parcel>().Where(item =>notAssign(item.DorneId));
        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a Parcel from the list
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel,Parcel newParcel)
        {
            DalObjectService.RemoveEntity(parcel);
            DalObjectService.AddEntity(newParcel);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            Parcel parcel = DalObjectService.GetEntities<Parcel>().FirstOrDefault(item => item.Id == id);
            if (parcel.Equals(default))
                throw new KeyNotFoundException($"The parcel {id} not exsits in the data");
            DalObjectService.RemoveEntity(parcel);
            parcel.IsNotActive = true;
            DalObjectService.AddEntity(parcel);
        }
    }
}
