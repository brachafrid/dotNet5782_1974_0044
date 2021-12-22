using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;
using BLApi;

namespace PL
{
   public static class DroneChargingHandler
    {
        public static BO.DroneInCharging ConvertBackDroneCharging(DroneInCharging droneInCharging)
        {
            return new BO.DroneInCharging()
            {
                Id = droneInCharging.Id,
                ChargingMode = droneInCharging.ChargingMode
            };
        }

        public static DroneInCharging ConvertDroneCharging(BO.DroneInCharging droneInCharging)
        {
            return new DroneInCharging()
            {
                Id = droneInCharging.Id,
                ChargingMode = droneInCharging.ChargingMode
            };
        }
    }
}
