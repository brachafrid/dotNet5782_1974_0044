using BLApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;

namespace PL
{
    public class DroneHandler
    {
        private static IBL ibal = BLFactory.GetBL();
        public void AddDrone(DroneAdd drone)
        {
            ibal.AddDrone(ConvertBackDroneToAdd(drone), drone.StationId);
        }

        public void UpdateDrone(int id, string model)
        {
            ibal.UpdateDrone(id, model);
        }

        public void SendDroneForCharg(int id)
        {
            ibal.SendDroneForCharg(id);
        }

        public void ReleaseDroneFromCharging(int id)
        {
            ibal.ReleaseDroneFromCharging(id);
        }

        public void DeleteDrone(int id)
        {
            ibal.DeleteDrone(id);
        }

        public Drone GetDrone(int id)
        {
           return ConvertDrone(ibal.GetDrone(id));
        }

        public IEnumerable<DroneToList> GetDrones()
        {
            return ibal.GetDrones().Select(item => ConvertDroneToList(item));
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

        public Drone ConvertDrone(BO.Drone drone)
        {
            return new Drone
            {
                Id = drone.Id,
                Model = drone.Model,
                BattaryMode=drone.BattaryMode,
                DroneState=(DroneState)drone.DroneState,
                Weight=(WeightCategories)drone.WeightCategory,
                Location=LocationHandler.ConvertLocation(drone.CurrentLocation),
                Parcel= drone.Parcel == null ? null : ParcelInTransferHandler.ConvertParcelInTransfer(drone.Parcel)
            };
        }

        public BO.Drone ConvertBackDrone(Drone drone)
        {
            return new BO.Drone
            {
                Id = drone.Id,
                Model = drone.Model,
                BattaryMode = drone.BattaryMode,
                DroneState = (BO.DroneState)drone.DroneState,
                WeightCategory = (BO.WeightCategories)drone.Weight,
                CurrentLocation = LocationHandler.ConvertBackLocation(drone.Location),
                Parcel =drone.Parcel==null?null:ParcelInTransferHandler.ConvertBackParcelInTransfer(drone.Parcel)
            };
        }

        public DroneToList ConvertDroneToList(BO.DroneToList drone)
        {
            return new DroneToList
            {
                Id = drone.Id,
                DroneModel = drone.DroneModel,
                BatteryState = drone.BatteryState,
                DroneState = (DroneState)drone.DroneState,
                Weight = (WeightCategories)drone.Weight,
                CurrentLocation = LocationHandler.ConvertLocation(drone.CurrentLocation),
                ParcelId = drone.ParcelId
            };
        }
        public BO.Drone ConvertBackDroneToAdd(DroneAdd drone)
        {
            return new()
            {
                Id = drone.Id != null ? (int)drone.Id : 0,
                Model = drone.Model,
                WeightCategory = (BO.WeightCategories)drone.Weight,
                DroneState = (BO.DroneState)drone.DroneState,

            };
        }
    }
}
