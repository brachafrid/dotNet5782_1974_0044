
namespace BO
{
    public class ParcelInTransfer
    {
        /// <summary>
        /// Parcel in transference key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Parcel in transference weight
        /// </summary>
        public WeightCategories WeightCategory { get; set; }
        /// <summary>
        /// Parcel in transference priority
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// Parcel in transference state
        /// </summary>
        public bool ParcelState { get; set; }
        /// <summary>
        /// Parcel in transference collection point
        /// </summary>
        public Location CollectionPoint { get; set; }
        /// <summary>
        /// Delivery destination of parcel in transference 
        /// </summary>
        public Location DeliveryDestination { get; set; }
        /// <summary>
        /// Transport distance
        /// </summary>
        public double TransportDistance { get; set; }
        /// <summary>
        /// Customer sender of parcel in transference
        /// </summary>
        public CustomerInParcel CustomerSender { get; set; }
        /// <summary>
        /// Customer receives of parcel in transference
        /// </summary>
        public CustomerInParcel CustomerReceives { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}


