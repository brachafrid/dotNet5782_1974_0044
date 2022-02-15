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
        /// <summary>
        /// parcel key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private CustomerInParcel customerSender;
        /// <summary>
        /// the customer that sended the parcel
        /// </summary>
        public CustomerInParcel CustomerSender
        {
            get => customerSender;
            set => Set(ref customerSender, value);
        }
        private CustomerInParcel customerReceives;
        /// <summary>
        /// the customer that gets the parcel
        /// </summary>
        public CustomerInParcel CustomerReceives
        {
            get => customerReceives;
            set => Set(ref customerReceives, value);
        }
        private WeightCategories weight;
        /// <summary>
        /// prcel weight
        /// </summary>
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private Priorities piority;
        /// <summary>
        /// the piority of sending parcel
        /// </summary>
        public Priorities Piority
        {
            get => piority;
            set => Set(ref piority, value);
        }
        private DroneWithParcel drone;
        /// <summary>
        /// the drone that took the parcel
        /// </summary>
        public DroneWithParcel Drone {
            get => drone;
            set => Set(ref drone, value);
             
        }
        private DateTime? creationTime;
        /// <summary>
        /// time of create the parcel
        /// </summary>
        public DateTime? CreationTime 
        { 
            get =>creationTime;
            set => Set(ref creationTime, value); 
        }
        private DateTime? assignmentTime;
        /// <summary>
        /// time of assigment the parcel to drone
        /// </summary>
        public DateTime? AssignmentTime
        {
            get => assignmentTime;
            set => Set(ref assignmentTime, value);
           
        }
        private DateTime? collectionTime;
        /// <summary>
        /// time of coolection parcel from sender
        /// </summary>
        public DateTime? CollectionTime
        {
            get => collectionTime;
            set => Set(ref collectionTime, value);
        }
        private DateTime? deliveryTime;
        /// <summary>
        /// time of bringing parcel to target
        /// </summary>
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


