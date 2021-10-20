using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    partial class DalObject
    {
       public void AssignParcelDrone(int parcelId)
        {
            Parcel tmpParcel=DataSorce.parcels.First(item => item.Id == parcelId);
            DataSorce.parcels.Remove(tmpParcel);
            Drone tmpDrone = DataSorce.drones.First(item => (tmpParcel.Weigth <= item.MaxWeight && item.Status == DroneStatuses.AVAILABLE));
            DataSorce.drones.Remove(tmpDrone);
            tmpParcel.DorneId = tmpDrone.Id;
            tmpDrone.Status = DroneStatuses.DELIVERY;
            tmpParcel.Sceduled = DateTime.Now;
            DataSorce.drones.Add(tmpDrone);
            DataSorce.parcels.Add(tmpParcel);
        }
        public void CollectParcel(int parcelId)
        {
            Parcel tmpParcel = DataSorce.parcels.First(item => item.Id == parcelId);
            DataSorce.parcels.Remove(tmpParcel);
            tmpParcel.PickedUp = DateTime.Now; 
            DataSorce.parcels.Add(tmpParcel);

        }
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
    }
}
