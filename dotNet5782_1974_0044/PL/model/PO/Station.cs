using System.Collections.Generic;
using System.ComponentModel;

namespace PL.PO
    {
        public class Station : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            init
            {
                id = value;
                onPropertyChanged("Id");
            }
        }
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                onPropertyChanged("Name");
            }
        }
        private int emptyChargeSlots;
        public int EmptyChargeSlots
        {
            get => emptyChargeSlots;
            set
            {
                emptyChargeSlots = value;
                onPropertyChanged("EmptyChargeSlots");
            }
        }

        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                location = value;
                onPropertyChanged("Location");
            }
        }

        private List<DroneInCharging> droneInChargings;

        public List<DroneInCharging> DroneInChargings
        {
            get => droneInChargings; 
            set {
                droneInChargings = value;
                onPropertyChanged("DroneInChargings");
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

