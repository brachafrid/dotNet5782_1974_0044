using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IblDrone
    {
        public void AddDrone(BO.Drone drone, int stationId);
        public void UpdateDrone(int id, string model);
        public void SendDroneForCharg(int id);
        public void ReleaseDroneFromCharging(int id, float timeOfCharg);
        public BO.Drone GetDrone(int id);
        public IEnumerable<BO.DroneToList> GetDrones();
        public void AssingParcellToDrone(int droneId);
        public void ParcelCollectionByDrone(int DroneId);
        public void DeliveryParcelByDrone(int droneId);
    }
}
