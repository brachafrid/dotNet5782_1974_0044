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
           public int Id { get; set; }
           public WeightCategories WeightCategory { get; set; }
           public Priorities Priority { get; set; }
           public PackageModes State { get; set; }
           public CustomerInParcel Customer { get; set; }

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


