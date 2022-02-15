using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PL.PO
{
    public class StationAdd : NotifyPropertyChangedBase, IDataErrorInfo
    {
        private int? id;
        /// <summary>
        /// Added station key
        /// </summary>
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Id
        {
            get => id;
            set => Set(ref id, value);
        }
        private string name;
        /// <summary>
        /// Added station name
        /// </summary>
        [Required(ErrorMessage = "required")]
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        private int? emptyChargeSlots;
        /// <summary>
        /// Added station empty charge slots
        /// </summary>
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? EmptyChargeSlots
        {
            get => emptyChargeSlots;
            set => Set(ref emptyChargeSlots, value);
        }

        private Location location = new();
        /// <summary>
        /// Added station location
        /// </summary>
        [Required(ErrorMessage = "required")]
        public Location Location
        {
            get => location;
            set => Set(ref location, value);
        }

        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] =>Validation.PropError(columnName, this);              

        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}

