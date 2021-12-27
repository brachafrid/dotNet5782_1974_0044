﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class Customer : INotifyPropertyChanged, IDataErrorInfo
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
        private string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                onPropertyChanged("Phone");
            }
        }

        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                location = value;
                onPropertyChanged("CollectionPoint");
            }
        }
        private List<ParcelAtCustomer> fromCustomer;
        public List<ParcelAtCustomer> FromCustomer
        {
            get => fromCustomer;
            set
            {
                fromCustomer = value;
                onPropertyChanged("FromCustomer");
            }
        }
        private List<ParcelAtCustomer> toCustomer;
        public List<ParcelAtCustomer> ToCustomer
        {
            get => toCustomer;
            set
            {
                toCustomer = value;
                onPropertyChanged("ToCustomer");
            }
        }

        public string Error
        {
            get
            {
                //foreach (var propertyInfo in GetType().GetProperties())
                //{
                //    if (!Validation.functions.FirstOrDefault(func => func.Key == propertyInfo.GetType()).Value(GetType().GetProperty(propertyInfo.Name).GetValue(this)))
                //        return "invalid" + propertyInfo.Name;
                //}
                return null;
            }
        }

        public string this[string columnName] =>null; /*Validation.functions.FirstOrDefault(func => func.Key == columnName.GetType()).Value(GetType().GetProperty(columnName).GetValue(this)) ? null : "invalid " + columnName;*/

        public override string ToString()
        {
            return this.ToStringProperties();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}


