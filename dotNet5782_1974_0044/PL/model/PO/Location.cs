using System;
using System.ComponentModel;
using PL;

namespace PL.PO
{
    public class Location : INotifyPropertyChanged, IDataErrorInfo
    {
        private double longitude;
        private double latitude;
        public double Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                onPropertyChanged("Longitude");

            }
        }
        
        public double Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                onPropertyChanged("Latitude");
            }
        }

        public string Error => "Location data erorr";

        public string this[string columnName]
        {
            get
            {
                
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
