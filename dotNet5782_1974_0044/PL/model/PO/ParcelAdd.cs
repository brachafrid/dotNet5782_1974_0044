using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class ParcelAdd : INotifyPropertyChanged, IDataErrorInfo
    {

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
        [Required(ErrorMessage = "required")]
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
        [Required(ErrorMessage = "required")]
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
        [Required(ErrorMessage = "required")]
        public Priorities Piority
        {
            get => piority;
            set
            {
                piority = value;
                onPropertyChanged("Piority");
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


