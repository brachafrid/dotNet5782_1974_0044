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
            public int Id { get; init; }
            public string DroneModel { get; set; }
            public WeightCategories Weight { get; set; }
            public double BatteryState { get; set; }
            public DroneState DroneState { get; set; }
            public Location CurrentLocation { get; set; }
            public int? ParcelId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }


