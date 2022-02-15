using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace PL.PO
    {
        public class ParcelAtCustomer : NotifyPropertyChangedBase
    {
        private int id;
        /// <summary>
        /// Parcel at customer key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private WeightCategories weight;
        /// <summary>
        /// Parcel at customer weight
        /// </summary>
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private Priorities piority;
        /// <summary>
        /// Parcel at customer piority
        /// </summary>
        public Priorities Piority
        {
            get => piority;
            set => Set(ref piority, value);
        }
        private PackageModes packageMode;
        /// <summary>
        /// Package mode of parcel at customer 
        /// </summary>
        public PackageModes PackageMode
        {
            get => packageMode;
            set => Set(ref packageMode, value);
        }

        private CustomerInParcel customer;
        /// <summary>
        /// The customer of the parcel
        /// </summary>
        public CustomerInParcel Customer
        {
            get => customer;
            set => Set(ref customer, value);
        }
        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }


