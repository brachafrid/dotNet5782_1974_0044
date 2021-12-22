using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace PL.PO
    {
      public  class ParcelToList : INotifyPropertyChanged
    {
            public int Id { get; init; }
            public Customer CustomerSender { get; set; }
            public Customer CustomerReceives { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Piority { get; set; }
            public PackageModes PackageMode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }


