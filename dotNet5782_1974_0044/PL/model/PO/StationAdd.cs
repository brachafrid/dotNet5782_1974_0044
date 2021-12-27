using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PL.PO
{
    public class StationAdd : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? id;
        public int? Id
        {
            get => id;
            set
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
        private int? emptyChargeSlots;
        public int? EmptyChargeSlots
        {
            get => emptyChargeSlots;
            set
            {
                emptyChargeSlots = value;
                onPropertyChanged("EmptyChargeSlots");
            }
        }

        private Location location = new();
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
                foreach (var item in GetType().GetProperties())
                {
                    if (this[item.Name] != null)
                        return "invalid" + item.Name;
                }
                return
                    null;
            }
        }

        public string this[string columnName]
        {

            get
            {
                if (Validation.functions.FirstOrDefault(func => func.Key == columnName).Value == default(Predicate<object>))
                    return null;
                var func = Validation.functions[columnName];
                return !func.Equals(default) ? func(GetType().GetProperty(columnName).GetValue(this)) ? null : "invalid " + columnName : null;

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

