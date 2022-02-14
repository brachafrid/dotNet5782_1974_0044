using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class ParcelToList : NotifyPropertyChangedBase
    {
        private int id;
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private Customer customerSender;
        public Customer CustomerSender
        {
            get => customerSender;
            set => Set(ref customerSender, value);
        }
        private Customer customerReceives;
        public Customer CustomerReceives
        {
            get => customerReceives;
            set => Set(ref customerReceives, value);
        }

        private WeightCategories weight;
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }

        private Priorities piority;
        public Priorities Piority {
            get => piority;
            set => Set(ref piority, value);
        }
        private PackageModes packageMode;
        public PackageModes PackageMode {
            get => packageMode;
            set => Set(ref packageMode, value);
        }

        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


