using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PL.PO
{
    public class StationAdd : INotifyPropertyChanged, IDataErrorInfo
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

        public string Error
        {
            get
            {
                foreach (var propertyInfo in GetType().GetProperties())
                {
                    if (!Validation.functions.FirstOrDefault(func => func.Key == propertyInfo.GetType()).Value(GetType().GetProperty(propertyInfo.Name).GetValue(this)))
                        return "invalid" + propertyInfo.Name;
                }
                return null;
            }
        }

        public string this[string columnName] => Validation.functions.FirstOrDefault(func => func.Key == columnName.GetType()).Value(this.GetType().GetProperty(columnName).GetValue(this)) ? null : "invalid " + columnName;
        

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

