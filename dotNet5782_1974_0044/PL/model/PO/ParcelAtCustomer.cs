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

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }


