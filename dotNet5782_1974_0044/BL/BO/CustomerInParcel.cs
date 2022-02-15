namespace BO
{
    public class CustomerInParcel
    {
        /// <summary>
        /// Key of customer in parcel
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Name of customer in parcel
        /// </summary>
        public string Name { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


