
using System.ComponentModel;

namespace PL.PO
{
    public class ParcelInTransfer : NotifyPropertyChangedBase
    {
        private int id;
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private WeightCategories weight;
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private Priorities piority;
        public Priorities Piority
        {
            get => piority;
            set => Set(ref piority, value);
        }
        private bool parcelState;
        public bool ParcelState {
            get => parcelState;
            set => Set(ref parcelState, value);
        }
        private Location collectionPoint;
        public Location CollectionPoint
        {
            get => collectionPoint;
            set => Set(ref collectionPoint, value);
        }
        private Location deliveryDestination;
        public Location DeliveryDestination
        {
            get => deliveryDestination;
            set => Set(ref deliveryDestination, value);
        }
        private double transportDistance;
        public double TransportDistance {
            get => transportDistance;
            set => Set(ref transportDistance, value);
        }
        private CustomerInParcel customerSender;
        public CustomerInParcel CustomerSender 
        {
            get => customerSender;
            set => Set(ref customerSender, value);
        }
        private CustomerInParcel customerReceives;
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


