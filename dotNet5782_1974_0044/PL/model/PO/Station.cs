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

