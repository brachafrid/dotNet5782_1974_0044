﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PL.PO
{
    public class Station : NotifyPropertyChangedBase,IDataErrorInfo
    {
        private int id;
        /// <summary>
        /// Station key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string name;
        /// <summary>
        /// Station name
        /// </summary>
        [Required(ErrorMessage = "required")]
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        private int emptyChargeSlots;
        /// <summary>
        /// Station empty charge slots
        /// </summary>
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int EmptyChargeSlots
        {
            get => emptyChargeSlots;
            set => Set(ref emptyChargeSlots, value);
        }

        private Location location;
        /// <summary>
        /// Station location
        /// </summary>
        public Location Location
        {
            get => location;
            set => Set(ref location, value);
        }

        private IEnumerable<DroneInCharging> droneInChargings;
        /// <summary>
        /// The list of the drones in charging of the station
        /// </summary>
        public IEnumerable<DroneInCharging> DroneInChargings
        {
            get => droneInChargings;
            set => Set(ref droneInChargings, value);
        }
        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);       

        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}

