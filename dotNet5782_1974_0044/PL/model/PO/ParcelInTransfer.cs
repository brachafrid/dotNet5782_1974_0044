
using System.ComponentModel;

namespace PL.PO
{
    public class ParcelInTransfer : NotifyPropertyChangedBase
    {
        private int id;
        /// <summary>
        /// Parcel in transference key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private WeightCategories weight;
        /// <summary>
        /// Parcel in transference weight
        /// </summary>
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private Priorities piority;
        /// <summary>
        /// Parcel in transference piority
        /// </summary>
        public Priorities Piority
        {
            get => piority;
            set => Set(ref piority, value);
        }
        private bool isParcelCollect;
        /// <summary>
        /// Parcel in transference state
        /// </summary>
        public bool IsParcelCollect {
            get => isParcelCollect;
            set => Set(ref isParcelCollect, value);
        }
        private Location collectionPoint;
        /// <summary>
        /// Parcel in transference collection point
        /// </summary>
        public Location CollectionPoint
        {
            get => collectionPoint;
            set => Set(ref collectionPoint, value);
        }
        private Location deliveryDestination;
        /// <summary>
        /// Delivery destination of the parcel in transference
        /// </summary>
        public Location DeliveryDestination
        {
            get => deliveryDestination;
            set => Set(ref deliveryDestination, value);
        }
        private double transportDistance;
        /// <summary>
        /// Transport distance
        /// </summary>
        public double TransportDistance {
            get => transportDistance;
            set => Set(ref transportDistance, value);
        }
        private CustomerInParcel customerSender;
        /// <summary>
        /// Customer sends of parcel in transference
        /// </summary>
        public CustomerInParcel CustomerSender 
        {
            get => customerSender;
            set => Set(ref customerSender, value);
        }
        private CustomerInParcel customerReceives;
        /// <summary>
        /// Customer receives of parcel in transference
        /// </summary>
        public CustomerInParcel CustomerReceives
        {
            get => customerReceives;
            set => Set(ref customerReceives, value);
        }

        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}


