using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneWithParcel : INotifyPropertyChanged
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
        private double chargingMode;

        public double ChargingMode
        {
            get => chargingMode;
            set
            {
                chargingMode = value;
                onPropertyChanged("ChargingMode");
            }
        }
        private Location currentLocation;
        public Location CurrentLocation
        {
            get => currentLocation;
            set
            {
                currentLocation = value;
                onPropertyChanged("CurrentLocation");
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

