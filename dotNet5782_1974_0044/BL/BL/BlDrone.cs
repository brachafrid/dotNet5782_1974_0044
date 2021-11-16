using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IblDrone
    {
        public void AddDrone(int id, string model, BO.WeightCategories MaximumWeight, int stationId)
        {
            dal.addDrone();
            throw new NotImplementedException();
        }
        public BO.Drone GetDrone(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            return MapDrone(dal.GetDrone(id));
        }
        public void ParcelCollectionByDrone(int DroneId)
        {
            throw new NotImplementedException();
        }
        public void ReleaseDroneFromCharging(int id, float timeOfCharg)
        {
            throw new NotImplementedException();
        }

        public void SendDroneForCharg(int id)
        {
            throw new NotImplementedException();
        }
        public void UpdateDrone(int id, string model)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Drone> GetDrones()
        {
            throw new NotImplementedException();
        }
        private List<DroneInCharging> CreatList(int id)
        {
            List<int>list=dal.GetDronechargingInStation(id);
            List<DroneInCharging> droneInChargings = new List<DroneInCharging>();
            DroneToList droneToList;
            foreach (var idDrone in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.Id == idDrone));
                if (!droneToList.Equals(default(DroneToList)))
                {
                    droneInChargings.Add(new DroneInCharging() { Id = idDrone, ChargingMode = droneToList.BatteryStatus });
                }
            }
            return droneInChargings;
        }
        private BO.Drone MapDrone(IDAL.DO.Drone drone)
        {
            DroneToList droneToList = drones.Find(item => item.Id == drone.Id);
            return new Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                WeightCategory = (WeightCategories)drone.MaxWeight,
                DroneStatus = droneToList.DroneStatus,
                BattaryMode = droneToList.BatteryStatus,
                CurrentLocation = droneToList.CurrentLocation,
                Parcel = droneToList.ParcellId!=null?CreateParcelInTransfer((int)droneToList.ParcellId):null
            };
        }
    }
}
