﻿

using System.ComponentModel;

namespace PL.PO
{
    public class StationToList : INotifyPropertyChanged
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
        private int fullChargeSlots;
        public int FullChargeSlots {
            get => fullChargeSlots;
            set
            {
                fullChargeSlots = value;
                onPropertyChanged("FullChargeSlots");
            } 
        }
        private int emptyChargeSlots;
        public int EmptyChargeSlots
        {
            get => emptyChargeSlots;
            set
            {
                emptyChargeSlots = value;
                onPropertyChanged("EmptyChargeSlots");
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


