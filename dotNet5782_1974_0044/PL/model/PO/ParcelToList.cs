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
        /// <summary>
        /// Parcel to list key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private Customer customerSender;
        /// <summary>
        /// Customer sends of parcel to list 
        /// </summary>
        public Customer CustomerSender
        {
            get => customerSender;
            set => Set(ref customerSender, value);
        }
        private Customer customerReceives;
        /// <summary>
        /// Customer receives of parcel to list 
        /// </summary>
        public Customer CustomerReceives
        {
            get => customerReceives;
            set => Set(ref customerReceives, value);
        }

        private WeightCategories weight;
        /// <summary>
        /// parcel to list weight
        /// </summary>
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }

        private Priorities piority;
        /// <summary>
        /// parcel to list piority
        /// </summary>
        public Priorities Piority {
            get => piority;
            set => Set(ref piority, value);
        }
        private PackageModes packageMode;
        /// <summary>
        /// Package mode of parcel to list 
        /// </summary>
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


