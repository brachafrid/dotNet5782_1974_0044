using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PL;

namespace PL.PO
{
    public class Location : NotifyPropertyChangedBase, IDataErrorInfo
    {
        private double? longitude;
        [Required(ErrorMessage = "required")]
        [Range(0, 90, ErrorMessage = "enter 0 - 90 longitude")]
        public double? Longitude
        {
            get => longitude;
            set => Set(ref longitude, value);
        }
        private double? latitude;
        [Required(ErrorMessage = "required")]
        [Range(-90, 90, ErrorMessage = "enter -90 -  90 latitude")]
        public double? Latitude
        {
            get => latitude;
            set => Set(ref latitude, value);
        }

        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}
