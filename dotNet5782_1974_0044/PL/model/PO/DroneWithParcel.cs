using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneWithParcel : NotifyPropertyChangedBase
    {
        private int id;
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private double chargingMode;

        public double ChargingMode
        {
            get => chargingMode;
            set => Set(ref chargingMode, value);
        }
        private Location currentLocation;
        public Location CurrentLocation
        {
            get => currentLocation;
            set => Set(ref currentLocation, value);
        }
        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}

