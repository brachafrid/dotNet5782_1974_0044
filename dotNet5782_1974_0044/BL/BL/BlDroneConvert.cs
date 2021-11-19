using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace IBL
{
    public partial class Bl
    {
        /// <summary>
        /// Convert a drone To List to Drone With Parcel
        /// </summary>
        /// <param name="drone">The drone to convert</param>
        /// <returns>The converter drone</returns>
        private DroneWithParcel mapDroneWithParcel(DroneToList drone)
        {
            return new DroneWithParcel()
            {
                CurrentLocation = drone.CurrentLocation,
                Id = drone.Id,
                ChargingMode = drone.BatteryStatus
            };
        }
    }
}
