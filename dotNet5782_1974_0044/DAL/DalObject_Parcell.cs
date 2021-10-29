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
        /// <summary>
        /// Gets parameters and create new parcel 
        /// </summary>
        /// <param name="SenderId"> Id of sener</param>
        /// <param name="TargetId"> Id of target</param>
        /// <param name="Weigth"> The weigth of parcel (light- 0,medium - 1,heavy - 2)</param>
        /// <param name="Priority"> The priority of send the parcel (regular - 0,fast - 1,emergency - 2)</param>
        public void ParcelsReception(int id, int SenderId, int TargetId, WeightCategories Weigth, Prioripies Priority)
        {
            uniqueIDTaxCheck<Parcel>(DataSorce.Parcels, id);
            DataSorce.Customers.First(item => item.Id == SenderId);
            DataSorce.Customers.First(item => item.Id == TargetId);
            Parcel newParcel = new Parcel();
            newParcel.Id = id;
            newParcel.SenderId = SenderId;
            newParcel.TargetId = TargetId;
            newParcel.Weigth = Weigth;
            newParcel.Priority = Priority;
            newParcel.Requested = DateTime.Now;
            newParcel.DorneId = 0;
            DataSorce.Parcels.Add(newParcel);
        }
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
            Drone tmpDrone = DataSorce.Drones.FirstOrDefault(item => (tmpParcel.Weigth <= item.MaxWeight));
            if (!(tmpDrone.Equals(default(Drone))))
            {
                DataSorce.Drones.Remove(tmpDrone);
                tmpParcel.DorneId = tmpDrone.Id;
                tmpParcel.Sceduled = DateTime.Now;
                DataSorce.Drones.Add(tmpDrone);
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
            Drone tmpDrone = DataSorce.Drones.FirstOrDefault(item => item.Id == tmpParcel.DorneId);
            if (!(tmpDrone.Equals(default(Drone))))
            {
                DataSorce.Drones.Remove(tmpDrone);
                DataSorce.Drones.Add(tmpDrone);
            }
        }
    }
}
