using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace PL.PO
    {
       public class Drone : INotifyPropertyChanged
    {
            public int Id { get; init; }
            public string Model { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public DroneState DroneState { get; set; }
            public double BattaryMode { get; set; }
            public Location CurrentLocation { get; set; }
            public ParcelInTransfer Parcel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }


