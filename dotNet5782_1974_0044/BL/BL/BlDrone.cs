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
            DroneToList droneToList = drones.Find(item => item.Id == id);
            if (droneToList.DroneStatus != DroneStatuses.MAINTENANCE)
                throw new Exception();
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatuses.AVAILABLE;
            droneToList.BatteryStatus += timeOfCharg/60*dal.GetElectricityUse()[4];
            dal.RemoveDroneCharge(id);
        }

        public void SendDroneForCharg(int id)
        {
            DroneToList droneToList = drones.Find(item => item.Id == id);
            if (droneToList.DroneStatus != DroneStatuses.AVAILABLE)
                throw new Exception();
            double minDistance;
            IDAL.DO.Station station=closetStation(dal.GetStations(), droneToList,out minDistance);
            if (station.Equals(default(IDAL.DO.Station)))
                throw new Exception();
            drones.Remove(droneToList);
            droneToList.CurrentLocation = new Location() { Longitude = station.Longitude, Latitude = station.Latitude };
            droneToList.DroneStatus = DroneStatuses.MAINTENANCE;
            droneToList.BatteryStatus -= minDistance * dal.GetElectricityUse()[0];
            //הורדת מספר עמדות טעינה בתחנה
            dal.AddDRoneCharge(id, station.Id);
        }
        public void UpdateDrone(int id, string name)
        {
            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Drone droneDl = dal.GetDrone(id);
            if (name.Equals(default(string)))
                throw new ArgumentNullException("For updating the name must be initialized ");
            dal.RemoveDrone(droneDl);
            dal.addDrone(id, name, droneDl.MaxWeight);
            DroneToList droneToList = drones.Find(item => item.Id == id);
            drones.Remove(droneToList);
            droneToList.DroneModel = name;
            drones.Add(droneToList);
        }
        public IEnumerable<DroneToList> GetDrones() => drones;
        private List<DroneInCharging> CreatListDroneInCharging(int id)
        {
            List<int> list = dal.GetDronechargingInStation(id);
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
                Parcel = droneToList.ParcellId != null ? CreateParcelInTransfer((int)droneToList.ParcellId) : null
            };
        }
        private IDAL.DO.Station closetStation(IEnumerable<IDAL.DO.Station> stations, DroneToList droneToList,out double minDistance)
        {
            minDistance = 0;
            double curDistance;
            IDAL.DO.Station station=default(IDAL.DO.Station);
            foreach (var item in stations)
            {
                curDistance = Distance(droneToList.CurrentLocation,
                    new Location() { Latitude = item.Latitude, Longitude = item.Longitude });
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    station = item;
                }
            }
            return minDistance*dal.GetElectricityUse()[0]< droneToList.BatteryStatus ? station : default(IDAL.DO.Station);
        }
        private DroneWithParcel mapDroneWithParcel(DroneToList drone)
        {
            return new DroneWithParcel()
            {
                CurrentLocation = drone.CurrentLocation,
                Id = drone.Id,
                ChargingMode = drone.BatteryStatus
            };
        }
        public void DeliveryParcelByDrone(int droneId)
        {
            throw new NotImplementedException();
        }
        public void AssingParcellToDrone(int droneId)
        {
            DroneToList aviableDrone = drones.Find(item => item.Id == droneId);
            DroneToList tempDrone = aviableDrone;
            List<ParcelInTransfer> parcels = (List<ParcelInTransfer>)CreateParcelInTransferList(aviableDrone.Weight);
            double emergency;
            foreach (var item in parcels)
            {
                if (item.Priority == Priorities.EMERGENCY)
                {
                    emergency = Distance(aviableDrone.CurrentLocation, item.CollectionPoint) * dal.GetElectricityUse()[0] +
                    Distance(item.CollectionPoint, item.DeliveryDestination) * dal.GetElectricityUse()[3];
                    tempDrone.BatteryStatus -= emergency;
                    emergency += closetStation(dal.GetStations(), tempDrone);
                    
                }
            }
        }
    }
}
