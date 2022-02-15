namespace BO
{
    public class ParcelToList
    {
        /// <summary>
        /// Parcel to list key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Customer sender of parcel to list
        /// </summary>
        public Customer CustomerSender { get; set; }
        /// <summary>
        /// Customer receives of parcel to list
        /// </summary>
        public Customer CustomerReceives { get; set; }
        /// <summary>
        /// Parcel to list weight
        /// </summary>
        public WeightCategories Weight { get; set; }
        /// <summary>
        /// Parcel to list priority
        /// </summary>
        public Priorities Piority { get; set; }
        /// <summary>
        /// Parcel to list mode
        /// </summary>
        public PackageModes PackageMode { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


