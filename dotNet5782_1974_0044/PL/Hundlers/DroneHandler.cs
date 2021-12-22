using BLApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Hundlers
{
    public class DroneHandler
    {
        private static IBL ibal = BLFactory.GetBL();
        public void AddDrone(BO.Drone drone, int stationId)
        {
            ibal.AddDrone(drone, stationId);
        }
        public void UpdateDrone(int id, string model)
        {
            ibal.UpdateDrone(id, model);
        }
        public void SendDroneForCharg(int id)
        {
            ibal.SendDroneForCharg(id);
        }
        public void ReleaseDroneFromCharging(int id, float timeOfCharg)
        {
            ibal.ReleaseDroneFromCharging(id, timeOfCharg);
        }
        public BO.Drone GetDrone(int id)
        {
           return ibal.GetDrone(id);
        }
        public IEnumerable<BO.DroneToList> GetDrones()
        {
            return ibal.GetDrones().Select(item => ConverterStationToList(item));
        }
        public void AssingParcelToDrone(int droneId)
        {
            ibal.AssingParcelToDrone(droneId);
        }
        public void ParcelCollectionByDrone(int droneId)
        {
            ibal.ParcelCollectionByDrone(droneId);
        }
        public void DeliveryParcelByDrone(int droneId)
        {
            ibal.DeliveryParcelByDrone(droneId);
        }
    }
}
