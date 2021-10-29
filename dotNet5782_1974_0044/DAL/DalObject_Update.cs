using System;
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
            Drone tmpDrone = DataSorce.Drones.FirstOrDefault(item => (tmpParcel.Weigth <= item.MaxWeight ));
            if(!(tmpDrone.Equals(default(Drone))))
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
        /// <summary>
        /// Sends drone to charge.
        /// Find available charge solt
        /// Create new droneCharge object, initializ it and add to DroneCharges list.
        /// Update the drone's status.
        /// </summary>
        /// <param name="droneId"> id of drone</param>
        public void SendDroneCharg(int droneId)
        {
            DroneCharge tmpDroneCharge = new DroneCharge();
            tmpDroneCharge.Droneld = droneId;
            tmpDroneCharge.Stationld = getAvailbleStaion().First().Id;
            DataSorce.DroneCharges.Add(tmpDroneCharge);
            Drone tmpDrone = DataSorce.Drones.First(item => item.Id == droneId);
            DataSorce.Drones.Remove(tmpDrone);
            DataSorce.Drones.Add(tmpDrone);
        }
        /// <summary>
        /// Releases the drone from charging.
        /// Update battary and status.
        /// Remove the droneCharge object from DroneCharges list
        /// </summary>
        /// <param name="droneId"> id of drone</param>
        public void ReleasDroneCharg(int droneId)
        {
            DataSorce.DroneCharges.Remove(DataSorce.DroneCharges.First(item => item.Droneld == droneId));
            Drone tmpDrone = DataSorce.Drones.FirstOrDefault(item => item.Id == droneId);
            if (!(tmpDrone.Equals(default(Drone))))
            {
                DataSorce.Drones.Remove(tmpDrone);
                DataSorce.Drones.Add(tmpDrone);
            }
        }

    }
}
