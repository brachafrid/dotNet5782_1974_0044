﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class Drone : INotifyPropertyChanged
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
        private double battaryMode;
        public double BattaryMode
        {
            get => battaryMode;
            set
            {
                battaryMode = value;
                onPropertyChanged("BattaryMode");
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
        private ParcelInTransfer parcel;
        public ParcelInTransfer Parcel 
        {
            get=>parcel;
            set
            {
                parcel = value;
                onPropertyChanged("Parcel");
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


