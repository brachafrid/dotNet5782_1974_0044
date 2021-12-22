using System;
using System.ComponentModel;

namespace PL.PO
{
    public class Location : INotifyPropertyChanged
    {
        private double longitude;
        private double latitude;
        public double Longitude
        {
            get => longitude;
            set
            {
                if (value >= 0 && value <= 90)
                {
                    longitude = value;
                    onPropertyChanged("Longitude");
                }
                else
                    throw new ArgumentOutOfRangeException("invalid longitude");
            }
        }
        public double Latitude
        {
            get => latitude;
            set
            {
                if (value > -90 && value <= 90)
                {
                    latitude = value;
                    onPropertyChanged("Latitude");
                } 
                else
                    throw new ArgumentOutOfRangeException("invalid latitude");
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
