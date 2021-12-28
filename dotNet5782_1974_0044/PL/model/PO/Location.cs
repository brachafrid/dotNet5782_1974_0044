using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PL;

namespace PL.PO
{
    public class Location : INotifyPropertyChanged, IDataErrorInfo
    {
        private double? longitude;
        [Required(ErrorMessage = "required")]
        [Range(0, 90, ErrorMessage = "enter 0 - 90 longitude")]
        public double? Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                onPropertyChanged("Longitude");

            }
        }
        private double? latitude;
        [Required(ErrorMessage = "required")]
        [Range(-90, 90, ErrorMessage = "enter -90 -  90 latitude")]
        public double? Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                onPropertyChanged("Latitude");
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
