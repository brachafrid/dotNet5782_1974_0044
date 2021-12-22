
using System.ComponentModel;

namespace PL.PO
    {
      public class ParcelInTransfer : INotifyPropertyChanged
    {
            public int Id { get; init; }
            public WeightCategories WeightCategory { get; set; }
            public Priorities Priority { get; set; }
            public bool ParcelState { get; set; }
            public Location CollectionPoint { get; set; }
            public Location DeliveryDestination { get; set; }
            public double TransportDistance { get; set; }
            public CustomerInParcel CustomerSender { get; set; }
            public CustomerInParcel CustomerReceives { get; set; }

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


