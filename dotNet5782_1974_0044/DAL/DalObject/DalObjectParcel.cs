using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void ParcelsReception( int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority,int id=0)
        {
            DataSorce.Customers.First(item => item.Id == SenderId);
            DataSorce.Customers.First(item => item.Id == TargetId);
            Parcel newParcel = new Parcel();
            newParcel.Id = ++DataSorce.Config.IdParcel;
            newParcel.SenderId = SenderId;
            newParcel.TargetId = TargetId;
            newParcel.Weigth = Weigth;
            newParcel.Priority = Priority;
            newParcel.Requested = DateTime.Now;
            newParcel.DorneId = 0;
            DataSorce.Parcels.Add(newParcel);
        }

        //-------------------------------------------Update-------------------------------------------
        /// <summary>
        /// Assign parcel to drone:
        /// Find suitable drone and 
        /// </summary>
        /// <param name="parcelId">Id of parcel</param>
        public void AssignParcelDrone(int parcelId)
        {

            Parcel tmpParcel = DataSorce.Parcels.First(item => item.Id == parcelId);
            if (tmpParcel.DorneId != 0)
                throw new ArgumentException("A drone already exists");
            DataSorce.Parcels.Remove(tmpParcel);
            Drone tmpDrone;
            findSuitableDrone(out tmpDrone, tmpParcel.Weigth);
            if (!(tmpDrone.Equals(default(Drone))))
            {
                tmpParcel.DorneId = tmpDrone.Id;
                tmpParcel.Sceduled = DateTime.Now;
            }
            DataSorce.Parcels.Add(tmpParcel);
        }


        /// <summary>
        /// collect parcel fo sending:
        /// update time of pick up parcel
        /// </summary>
        /// <param name="parcelId">id of parcel</param>
        public void CollectParcel(int parcelId)
        {
            Parcel tmpParcel = DataSorce.Parcels.First(item => item.Id == parcelId);
            if (tmpParcel.DorneId == 0)
                throw new ArgumentException("dosent have a drone");
            if (!tmpParcel.PickedUp.Equals(new DateTime()))
                throw new ArgumentException("already picked up");
            DataSorce.Parcels.Remove(tmpParcel);
            tmpParcel.PickedUp = DateTime.Now;
            DataSorce.Parcels.Add(tmpParcel);
        }
        /// <summary>
        /// Supply parcel to customer:
        /// Releases the drone,
        /// Update the time of delivered.
        /// </summary>
        /// <param name="parcelId"> Id of parcel</param>
        public void SupplyParcel(int parcelId)
        {
            Parcel tmpParcel = DataSorce.Parcels.First(item => item.Id == parcelId);
            if (tmpParcel.DorneId == 0)
                throw new ArgumentException("dosent have a drone");
            if (tmpParcel.PickedUp.Equals(new DateTime()))
                throw new ArgumentException("parcel not picked up");
            if (!tmpParcel.Delivered.Equals(new DateTime()))
                throw new ArgumentException("already delivered");
            DataSorce.Parcels.Remove(tmpParcel);
            tmpParcel.Delivered = DateTime.Now;
            DataSorce.Parcels.Add(tmpParcel);
            
        }
        //-----------------------------------------------------Display--------------------------------------
        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        public Parcel GetParcel(int id)=>DataSorce.Parcels.First(item => item.Id == id);

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
