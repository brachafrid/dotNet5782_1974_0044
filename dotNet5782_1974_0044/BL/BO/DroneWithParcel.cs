namespace BO
{
    public class DroneWithParcel
    {
        /// <summary>
        /// Drone with parcel key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Drone with parcel charging mode
        /// </summary>
        public double ChargingMode { get; set; }
        /// <summary>
        /// Drone with parcel current location
        /// </summary>
        public Location CurrentLocation { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}

