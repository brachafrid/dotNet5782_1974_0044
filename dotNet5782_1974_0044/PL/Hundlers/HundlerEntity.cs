using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;
using BLApi;

namespace PL
{
    public static class HundlerEntity
    {
        static IBL ibal = BLFactory.GetBL();
        public static BO.Location ConvertLocation(Location location)
        {
            return new BO.Location()
            {
               Longitude=location.Longitude,
               Latitude=location.Latitude
            };
        }

        public static BO.DroneInCharging ConvertLocation(DroneInCharging droneInCharging)
        {
            return new BO.DroneInCharging()
            {
                Id = droneInCharging.Id,
                ChargingMode = droneInCharging.ChargingMode
            };
        }
    }
}
