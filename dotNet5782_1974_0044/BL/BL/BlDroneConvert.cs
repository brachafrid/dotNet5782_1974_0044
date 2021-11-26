using IBL.BO;

namespace IBL
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
                ChargingMode = drone.BatteryStatus,
                CurrentLocation = drone.CurrentLocation
            };
        }
    }
}
