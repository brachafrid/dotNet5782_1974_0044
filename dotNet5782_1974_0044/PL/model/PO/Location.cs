using System;
using System.ComponentModel;
using System.Linq;
using PL;

namespace PL.PO
{
    public class Location : INotifyPropertyChanged, IDataErrorInfo
    {
        private double? longitude;
        private double? latitude;
        public double? Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                onPropertyChanged("Longitude");

            }
        }
        
        public double? Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                onPropertyChanged("Latitude");
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
