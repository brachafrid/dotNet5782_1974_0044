using BO;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// Convert a drone To List to Drone With Parcel
        /// </summary>
        /// <param name="drone">The drone to convert</param>
        /// <returns>The converter drone</returns>
        private DroneWithParcel MapDroneWithParcel(DroneToList drone)
        {
            return new DroneWithParcel()
            {
                Id = drone.Id,
                ChargingMode = drone.BatteryState,
                CurrentLocation = drone.CurrentLocation
            };
        }

        /// <summary>
        /// Convert a Bl Drone To List to BL drone
        /// </summary>
        /// <param name="drone">The drone to convert</param>
        /// <returns>The converted drone</returns>
        private Drone MapDrone(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default || droneToList.IsNotActive)
                throw new KeyNotFoundException($"Map drone: There is not drone with same id in the data , the id {id}");
            return new Drone()
            {
                Id = droneToList.Id,
                Model = droneToList.DroneModel,
                WeightCategory = droneToList.Weight,
                DroneState = droneToList.DroneState,
                BattaryMode = droneToList.BatteryState,
                CurrentLocation = droneToList.CurrentLocation,
                Parcel = droneToList.ParcelId != 0 ? CreateParcelInTransfer((int)droneToList.ParcelId) : null
            };

        }
    }
}
