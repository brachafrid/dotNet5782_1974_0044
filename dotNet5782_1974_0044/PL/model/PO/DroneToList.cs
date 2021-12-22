using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneToList : INotifyPropertyChanged
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
        private string droneModel;
        public string DroneModel 
        {
            get => droneModel;
            set
            {
                droneModel = value;
                onPropertyChanged("DroneModel");
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
        private double batteryState;
        public double BatteryState {
            get => batteryState;
            set
            {
                batteryState = value;
                onPropertyChanged("BatteryState");
            } 
        }
        private DroneState droneState;
        public DroneState DroneState 
        { 
            get =>droneState; 
            set
            {
                droneState = value;
                onPropertyChanged("DroneState");
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
        private int? parcelId;
        public int? ParcelId 
        { 
            get => parcelId; 
            set
            {
                parcelId = value;
                onPropertyChanged("ParcelId");
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


