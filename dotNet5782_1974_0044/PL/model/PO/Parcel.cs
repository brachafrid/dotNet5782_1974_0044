using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class Parcel : NotifyPropertyChangedBase,IDataErrorInfo
    { 
        private int id;
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private CustomerInParcel customerSender;
        public CustomerInParcel CustomerSender
        {
            get => customerSender;
            set => Set(ref customerSender, value);
        }
        private CustomerInParcel customerReceives;
        public CustomerInParcel CustomerReceives
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
        public Priorities Piority
        {
            get => piority;
            set => Set(ref piority, value);
        }
        private DroneWithParcel drone;
        public DroneWithParcel Drone {
            get => drone;
            set => Set(ref drone, value);
             
        }
        private DateTime? creationTime;
        public DateTime? CreationTime 
        { 
            get =>creationTime;
            set => Set(ref creationTime, value); 
        }
        private DateTime? assignmentTime;
        public DateTime? AssignmentTime
        {
            get => assignmentTime;
            set => Set(ref assignmentTime, value);
           
        }
        private DateTime? collectionTime;
        public DateTime? CollectionTime
        {
            get => collectionTime;
            set => Set(ref collectionTime, value);
        }
        private DateTime? deliveryTime;
        public DateTime? DeliveryTime
        {
            get => deliveryTime;
            set => Set(ref deliveryTime, value);
        }
        public string Error
        {
            get => null;
        }

        public string this[string columnName] =>null; 

        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}


