using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PL.PO
{
    public class Station : INotifyPropertyChanged,IDataErrorInfo
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
        [Required(ErrorMessage = "required")]
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
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
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
        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);

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

