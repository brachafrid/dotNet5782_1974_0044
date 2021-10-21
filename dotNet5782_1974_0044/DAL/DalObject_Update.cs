﻿using System;
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
        /// assign parcel to drone:
        /// find suitable drone and 
        /// </summary>
        /// <param name="parcelId"></param>
        public void AssignParcelDrone(int parcelId)
        {
            Parcel tmpParcel = DataSorce.parcels.First(item => item.Id == parcelId);
            DataSorce.parcels.Remove(tmpParcel);
            Drone tmpDrone = DataSorce.drones.First(item => (tmpParcel.Weigth <= item.MaxWeight && item.Status == DroneStatuses.AVAILABLE));
            DataSorce.drones.Remove(tmpDrone);
            tmpParcel.DorneId = tmpDrone.Id;
            tmpDrone.Status = DroneStatuses.DELIVERY;
            tmpParcel.Sceduled = DateTime.Now;
            DataSorce.drones.Add(tmpDrone);
            DataSorce.parcels.Add(tmpParcel);
        }
        /// <summary>
        /// collect parcel fo sending:
        /// update time of pick up parcel
        /// </summary>
        /// <param name="parcelId">id of parcel</param>
        public void CollectParcel(int parcelId)
        {
            Parcel tmpParcel = DataSorce.parcels.First(item => item.Id == parcelId);
            DataSorce.parcels.Remove(tmpParcel);
            tmpParcel.PickedUp = DateTime.Now;
            DataSorce.parcels.Add(tmpParcel);
        }
        /// <summary>
        /// supply parcel to customer:
        /// Releases the drone,
        /// update the time of delivered.
        /// </summary>
        /// <param name="parcelId"> id of parcel</param>
        public void SupplyParcel(int parcelId)
        {
            Parcel tmpParcel = DataSorce.parcels.First(item => item.Id == parcelId);
            DataSorce.parcels.Remove(tmpParcel);
            tmpParcel.Delivered = DateTime.Now;
            DataSorce.parcels.Add(tmpParcel);
            Drone tmpDrone = DataSorce.drones.First(item => item.Id == tmpParcel.DorneId);
            DataSorce.drones.Remove(tmpDrone);
            tmpDrone.Status = DroneStatuses.AVAILABLE;
            DataSorce.drones.Add(tmpDrone);
        }
        /// <summary>
        /// sends drone to charge.
        /// find available charge solt
        /// create new droneCharge object, initializing it and add to droneCharges list.
        /// update the drone's status.
        /// </summary>
        /// <param name="droneId"> id of drone</param>
        public void SendingDroneCharging(int droneId)
        {
            DroneCharge tmpDroneCharge = new DroneCharge();
            tmpDroneCharge.Droneld = droneId;
            tmpDroneCharge.Stationld = getAvailbleStations().First().Id;
            DataSorce.droneCharges.Add(tmpDroneCharge);
            Drone tmpDrone = DataSorce.drones.First(item => item.Id == droneId);
            DataSorce.drones.Remove(tmpDrone);
            tmpDrone.Status = DroneStatuses.MAINTENANCE;
            DataSorce.drones.Add(tmpDrone);
        }
        /// <summary>
        /// releases the drone from charging.
        /// update battary and status.
        /// remove the droneCharge object from droneCharges list
        /// </summary>
        /// <param name="droneId"> id of drone</param>
        public void ReleasingDroneCharging(int droneId)
        {
            DataSorce.droneCharges.Remove(DataSorce.droneCharges.First(item => item.Droneld == droneId));
            Drone tmpDrone = DataSorce.drones.First(item => item.Id == droneId);
            DataSorce.drones.Remove(tmpDrone);
            tmpDrone.Status = DroneStatuses.AVAILABLE;
            tmpDrone.Battery = 100;
            DataSorce.drones.Add(tmpDrone);
        }

    }
}
