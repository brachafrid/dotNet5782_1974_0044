
using System.Collections.Generic;
using System;




namespace BLApi
{
    public interface IBlDrone
    {
        public void AddDrone(BO.Drone drone, int stationId);
        public void UpdateDrone(int id, string model);
        public void SendDroneForCharg(int id);
        public void ReleaseDroneFromCharging(int id);
        public BO.Drone GetDrone(int id);
        public IEnumerable<BO.DroneToList> GetDrones();
        public IEnumerable<BO.DroneToList> GetActiveDrones();
        public void AssingParcelToDrone(int droneId);
        public void ParcelCollectionByDrone(int DroneId);
        public void DeliveryParcelByDrone(int droneId);
        public void DeleteDrone(int id);
        public bool IsNotActiveDrone(int id);

    }
}


