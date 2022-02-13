using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneInCharging : NotifyPropertyChangedBase
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
            get=>chargingMode; 
            set => 
            {
                chargingMode = value;
                onPropertyChanged("ChargingMode");
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


