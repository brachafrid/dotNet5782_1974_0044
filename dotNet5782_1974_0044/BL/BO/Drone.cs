namespace BO
{
    public class Drone
    {
        /// <summary>
        /// Drone id
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Drone model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Drone weight
        /// </summary>
        public WeightCategories WeightCategory { get; set; }
        /// <summary>
        /// Drone state
        /// </summary>
        public DroneState DroneState { get; set; }
        /// <summary>
        /// Drone battery mode
        /// </summary>
        public double BattaryMode { get; set; }
        /// <summary>
        /// Drone current location
        /// </summary>
        public Location CurrentLocation { get; set; }
        /// <summary>
        /// The parcel of the drone
        /// </summary>
        public ParcelInTransfer Parcel { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


