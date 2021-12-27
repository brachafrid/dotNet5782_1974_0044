using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class Parcel : INotifyPropertyChanged,IDataErrorInfo
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
        private CustomerInParcel customerSender;
        public CustomerInParcel CustomerSender
        {
            get => customerSender;
            set
            {
                customerSender = value;
                onPropertyChanged("CustomerSender");
            }
        }
        private CustomerInParcel customerReceives;
        public CustomerInParcel CustomerReceives
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
        public Priorities Piority
        {
            get => piority;
            set
            {
                piority = value;
                onPropertyChanged("Piority");
            }
        }
        private DroneWithParcel drone;
        public DroneWithParcel Drone {
            get => drone;
            set
            {
                drone = value;
                onPropertyChanged("Drone");
            } 
        }
        private DateTime? creationTime;
        public DateTime? CreationTime 
        { 
            get =>creationTime;
            set
            {
                creationTime = value;
                onPropertyChanged("CreationTime");
            } 
        }
        private DateTime? assignmentTime;
        public DateTime? AssignmentTime
        {
            get => assignmentTime;
            set
            {
                assignmentTime = value;
                onPropertyChanged("AssignmentTime");
            }
        }
        private DateTime? collectionTime;
        public DateTime? CollectionTime
        {
            get => collectionTime;
            set
            {
                collectionTime = value;
                onPropertyChanged("CollectionTime");
            }
        }
        private DateTime? deliveryTime;
        public DateTime? DeliveryTime
        {
            get => deliveryTime;
            set
            {
                deliveryTime = value;
                onPropertyChanged("DeliveryTime");
            }
        }
        public string Error
        {
            get
            {
                //foreach (var propertyInfo in GetType().GetProperties())
                //{
                //    if (!Validation.functions.FirstOrDefault(func => func.Key == propertyInfo.GetType()).Value(GetType().GetProperty(propertyInfo.Name).GetValue(this)))
                //        return "invalid" + propertyInfo.Name;
                //}
                return null;
            }
        }

        public string this[string columnName] =>null; /*Validation.functions.FirstOrDefault(func => func.Key == columnName.GetType()).Value(this.GetType().GetProperty(columnName).GetValue(this)) ? null : "invalid " + columnName;*/

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


