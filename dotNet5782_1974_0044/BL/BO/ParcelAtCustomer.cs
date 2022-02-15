namespace BO
{
    public class ParcelAtCustomer
    {
        /// <summary>
        /// Parcel at customer key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Parcel at customer weight
        /// </summary>
        public WeightCategories WeightCategory { get; set; }
        /// <summary>
        /// Parcel at customer priority
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// Parcel at customer state
        /// </summary>
        public PackageModes State { get; set; }
        /// <summary>
        /// The customer in Parcel
        public CustomerInParcel Customer { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


