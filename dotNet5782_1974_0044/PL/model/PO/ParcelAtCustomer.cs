using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace PL.PO
    {
        public class ParcelAtCustomer : INotifyPropertyChanged
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
        private Priorities piority;
        public Priorities Piority
        {
            get => piority;
            set
            {
                piority = value;
                onPropertyChanged("Piority");
            }
        }
        private PackageModes packageMode;
        public PackageModes PackageMode
        {
            get => packageMode;
            set
            {
                packageMode = value;
                onPropertyChanged("PackageMode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
        private CustomerInParcel customer;
        public CustomerInParcel Customer
        {
            get => customer;
            set
            {
                customer = value;
                onPropertyChanged("Customer");
            }
        }
        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }


