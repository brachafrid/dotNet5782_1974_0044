using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneToList : NotifyPropertyChangedBase
    {
        private int id;
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string droneModel;
        public string DroneModel 
        {
            get => droneModel;
            set => Set(ref droneModel, value); 
        }
        private WeightCategories weight;
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private double batteryState;
        public double BatteryState {
            get => batteryState;
            set => Set(ref batteryState, value); 
        }
        private DroneState droneState;
        public DroneState DroneState 
        { 
            get =>droneState;
            set => Set(ref droneState, value); 
        }
        private Location currentLocation;
        public Location CurrentLocation
        {
            get => currentLocation;
            set => Set(ref currentLocation, value);
        }
        private int? parcelId;
        public int? ParcelId 
        { 
            get => parcelId;
            set => Set(ref parcelId, value); 
        }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


