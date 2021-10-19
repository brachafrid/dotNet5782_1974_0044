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
       public void AssignParcelDrone(Parcel parcel)
        {
            for (int i = 0; i < DataSorce.Config.idxDrones; i++)
            {
                if (parcel.Weigth <= DataSorce.drones[i].MaxWeight && DataSorce.drones[i].Status == DroneStatuses.AVAILABLE)
                {
                    parcel.DorneId = DataSorce.drones[i].Id;
                    Drone newDrone = DataSorce.drones[i];
                    newDrone.Status = DroneStatuses.DELIVERY;
                    DataSorce.drones[i] = newDrone;
                }
            }
            //foreach (Drone item in DataSorce.drones)
            //{
            //    if (parcel.Weigth <= item.MaxWeight && item.Status == DroneStatuses.AVAILABLE)
            //    {
            //        parcel.DorneId = item.Id;
            //        item.Status = DroneStatuses.DELIVERY;
            //        parcel.Sceduled = DateTime.Now;
            //    }
            //}
        }
        public void CollectParcel(Parcel parcel)
        {
            parcel.PickedUp = DateTime.Now;
        }
        public void SupplyParcel(Parcel parcel)
        {
            parcel.Delivered = DateTime.Now;
            Drone newDrone = DataSorce.drones.First(item => item.Id == parcel.DorneId);
            newDrone.Status = DroneStatuses.AVAILABLE;


        }
    }
}
