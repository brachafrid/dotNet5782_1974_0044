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

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }


