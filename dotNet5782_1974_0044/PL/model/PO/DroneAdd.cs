﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneAdd : INotifyPropertyChanged, IDataErrorInfo
    {
        Dictionary<string, Func<object,bool>> _validationColumns;
        public DroneAdd()
        {
            _validationColumns = new Dictionary<string, Func<object, bool>>();
            _validationColumns.Add(nameof(Id),(input)=> Validation.NumberValid((int)input));
        }
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
        private string model;

        public string Model
        {
            get => model;
            set
            {
                model = value;
                onPropertyChanged("Model");
            }
        }
        private WeightCategories weight;
        public WeightCategories Weight
        {
            get => weight;
            set
            {
                weight = value;
                onPropertyChanged("Weight");
            }
        }
        private DroneState droneState;
        public DroneState DroneState
        {
            get => droneState;
            set
            {
                droneState = value;
                onPropertyChanged("DroneState");
            }
        }
        
        public string Error => "invalid parameter";

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


