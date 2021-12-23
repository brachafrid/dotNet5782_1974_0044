using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;

namespace PL
{
    public static class DroneWithParcelHandler
    {
        public static DroneWithParcel ConvertDroneWithParcel(BO.DroneWithParcel drone)
        {
            return new DroneWithParcel()
            {
                Id = drone.Id,
                ChargingMode = drone.ChargingMode,
                CurrentLocation = LocationHandler.ConvertLocation(drone.CurrentLocation)
            };
        }
        public static BO.DroneWithParcel ConvertBackDroneWithParcel(DroneWithParcel drone)
        {
            return new()
            {
                Id = drone.Id,
                ChargingMode = drone.ChargingMode,
                CurrentLocation = LocationHandler.ConvertBackLocation(drone.CurrentLocation)
            };
        }
    }
}
