using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class ParcelToList : INotifyPropertyChanged
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
        private Customer customerSender;
        public Customer CustomerSender
        {
            get => customerSender;
            set
            {
                customerSender = value;
                onPropertyChanged("CustomerSender");
            }
        }
        private Customer customerReceives;
        public Customer CustomerReceives
        {
            get => customerReceives;
            set
            {
                customerReceives = value;
                onPropertyChanged("CustomerReceives");
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
        public Priorities Piority {
            get => piority;
            set
            {
                piority = value;
                onPropertyChanged("Piority");
            }
        }
        private PackageModes packageMode;
        public PackageModes PackageMode {
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
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


