using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneToList
    {
        /// <summary>
        /// Drone to list key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Drone to list model
        /// </summary>
        public string DroneModel { get; set; }
        /// <summary>
        /// Drone to list weight
        /// </summary>
        public WeightCategories Weight { get; set; }
        /// <summary>
        /// Drone to list battery state
        /// </summary>
        private double batteryState;
        /// <summary>
        /// Drone to list state
        /// </summary>
        public DroneState DroneState { get; set; }
        /// <summary>
        /// Drone to list current location
        /// </summary>
        public Location CurrentLocation { get; set; }
        /// <summary>
        /// If the drone to list is not active
        /// </summary>
        public bool IsNotActive { get; set; }
        /// <summary>
        /// The parcel id of the drone to list
        /// </summary>
        public int? ParcelId { get; set; }
        /// <summary>
        /// Drone to list battery state
        /// </summary>
        public double BatteryState 
        { 
            get => batteryState; 
            set=> batteryState = value<100? value<0?0:value:100;       
        }

        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


