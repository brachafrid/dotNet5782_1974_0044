

namespace DO
{
    public struct Drone : IIdentifyable, IActiveable
    {
        /// <summary>
        /// The drone Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The drone model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// The max weight of parcel that the drone can take
        /// </summary>
        public WeightCategories MaxWeight { get; set; }
        /// <summary>
        /// Is the Drone active
        /// </summary>
        public bool IsNotActive { get; set; }
        public override string ToString() => $"Drone ID:{Id} Model:{Model}";

    }
}


