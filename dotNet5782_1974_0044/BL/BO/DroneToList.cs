using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneToList
    {
        public int Id { get; init; }
        public string DroneModel { get; set; }
        public WeightCategories Weight { get; set; }
        private double batteryState;
        public DroneState DroneState { get; set; }
        public Location CurrentLocation { get; set; }
        public bool IsNotActive { get; set; }
        public int? ParcelId { get; set; }
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


