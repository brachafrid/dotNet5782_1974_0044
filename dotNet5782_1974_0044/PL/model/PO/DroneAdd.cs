﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneAdd : NotifyPropertyChangedBase, IDataErrorInfo
    {
        private int? id;
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Id
        {
            get => id;
            set => Set(ref id, value);
        }

        private int stationId;
        [Required(ErrorMessage = "required")]
        public int StationId
        {
            get { return stationId; }
            set => Set(ref stationId, value);
        }

        private string model;
        [Required(ErrorMessage = "required")]
        public string Model
        {
            get => model;
            set => Set(ref model, value);
        }

        private WeightCategories? weight;
        [Required(ErrorMessage = "required")]
        public WeightCategories? Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }

        private DroneState droneState;
        [Required(ErrorMessage = "required")]
        public DroneState DroneState
        {
            get => droneState;
            set =>Set(ref droneState, value);
        }
        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


