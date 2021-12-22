using System.Collections.Generic;
using System.ComponentModel;

namespace PL.PO
    {
        public class Station : INotifyPropertyChanged
    {
            public int Id { get; init; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int AvailableChargingPorts { get; set; }
            public List<DroneInCharging> DroneInChargings { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

