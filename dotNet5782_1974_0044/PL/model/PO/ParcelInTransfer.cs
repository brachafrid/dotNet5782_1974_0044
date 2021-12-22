
using System.ComponentModel;

namespace PL.PO
{
    public class ParcelInTransfer : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            init
            {
                id = value;
                onPropertyChanged("Id");
            }
        }
        private WeightCategories weight;
        public WeightCategories Weight
        {
            get => weight;
            set
            {
                weight = value;
                onPropertyChanged("Weight");
            }
        }
        private Priorities piority;
        public Priorities Piority
        {
            get => piority;
            set
            {
                piority = value;
                onPropertyChanged("Piority");
            }
        }
        private bool parcelState;
        public bool ParcelState {
            get => parcelState;
            set
            {
                parcelState = value;
                onPropertyChanged("ParcelState");
            }
        }
        private Location collectionPoint;
        public Location CollectionPoint
        {
            get => collectionPoint;
            set
            {
                collectionPoint = value;
                onPropertyChanged("CollectionPoint");
            }
        }
        private Location deliveryDestination;
        public Location DeliveryDestination
        {
            get => deliveryDestination;
            set
            {
                deliveryDestination = value;
                onPropertyChanged("DeliveryDestination");
            }
        }
        private double transportDistance;
        public double TransportDistance {
            get => transportDistance;
            set
            {
                transportDistance = value;
                onPropertyChanged("TransportDistance");
            }
        }
        private CustomerInParcel customerSender;
        public CustomerInParcel CustomerSender 
        {
            get => customerSender;
            set
            {
                customerSender = value;
                onPropertyChanged("CustomerSender");
            } 
        }
        private CustomerInParcel customerReceives;
        public CustomerInParcel CustomerReceives
        {
            get => customerReceives;
            set
            {
                customerReceives = value;
                onPropertyChanged("CustomerReceives");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }

        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}


