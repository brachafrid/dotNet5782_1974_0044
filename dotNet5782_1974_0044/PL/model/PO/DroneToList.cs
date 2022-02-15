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
        /// <summary>
        /// Drone to list key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string droneModel;
        /// <summary>
        /// Drone to list model
        /// </summary>
        public string DroneModel 
        {
            get => droneModel;
            set => Set(ref droneModel, value); 
        }
        private WeightCategories weight;
        /// <summary>
        /// Drone to list weight
        /// </summary>
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private double batteryState;
        /// <summary>
        /// Drone to list battery state
        /// </summary>
        public double BatteryState {
            get => batteryState;
            set => Set(ref batteryState, value); 
        }
        private DroneState droneState;
        /// <summary>
        /// Drone to list state
        /// </summary>
        public DroneState DroneState 
        { 
            get =>droneState;
            set => Set(ref droneState, value); 
        }
        private Location currentLocation;
        /// <summary>
        /// Drone to list current location
        /// </summary>
        public Location CurrentLocation
        {
            get => currentLocation;
            set => Set(ref currentLocation, value);
        }
        private int? parcelId;
        /// <summary>
        /// parcel key of the drone to list 
        /// </summary>
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


