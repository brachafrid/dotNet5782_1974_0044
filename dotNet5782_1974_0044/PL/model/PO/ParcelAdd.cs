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
        private int customerSender;
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int CustomerSender
        {
            get => customerSender;
            set
            {
                customerSender = value;
                onPropertyChanged("CustomerSender");
            }
        }
        private int customerReceives;
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int CustomerReceives
        {
            get => customerReceives;
            set
            {
                customerReceives = value;
                onPropertyChanged("CustomerReceives");
            }
        }
        private WeightCategories? weight;
        [Required(ErrorMessage = "required")]
        public WeightCategories? Weight
        {
            get => weight;
            set
            {
                weight = value;
                onPropertyChanged("Weight");
            }
        }
        private Priorities? piority;
        [Required(ErrorMessage = "required")]
        public Priorities? Piority
        {
            get => piority;
            set
            {
                piority = value;
                onPropertyChanged("Piority");
            }
        }
        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);

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


