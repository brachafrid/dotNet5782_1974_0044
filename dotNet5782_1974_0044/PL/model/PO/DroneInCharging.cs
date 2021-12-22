using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace PL.PO
    {
        public class DroneInCharging : INotifyPropertyChanged
    {
            public int Id { get; init; }
            public double ChargingMode { get; set; }

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


