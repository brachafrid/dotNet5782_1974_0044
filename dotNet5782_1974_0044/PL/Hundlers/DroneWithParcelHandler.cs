using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;

namespace PL.Hundlers
{
    class DroneWithParcelHandler
    {
        DroneWithParcel ConvertDroneWithParcel(BO.DroneWithParcel drone)
        {
            return new DroneWithParcel()
            {
                Id = drone.Id,
                ChargingMode = drone.ChargingMode,
                CurrentLocation = LocationHandler.ConvertLocation(drone.CurrentLocation)
            };
        }
        BO.DroneWithParcel ConvertBackDroneWithParcel(DroneWithParcel drone)
        {
            return new()
            {
                Id = drone.Id,
                ChargingMode = drone.ChargingMode,
                CurrentLocation = LocationHandler.ConvertBackLocation(drone.CurrentLocation);
            }
        }
    }
}
