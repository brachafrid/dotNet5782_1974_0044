using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class Customer : INotifyPropertyChanged, IDataErrorInfo
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
        private string name;
        [Required(ErrorMessage = "required")]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                onPropertyChanged("Name");
            }
        }
        private string phone;
        [Required(ErrorMessage = "required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                onPropertyChanged("Phone");
            }
        }

        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                location = value;
                onPropertyChanged("CollectionPoint");
            }
        }
        private List<ParcelAtCustomer> fromCustomer;
        public List<ParcelAtCustomer> FromCustomer
        {
            get => fromCustomer;
            set
            {
                fromCustomer = value;
                onPropertyChanged("FromCustomer");
            }
        }
        private List<ParcelAtCustomer> toCustomer;
        public List<ParcelAtCustomer> ToCustomer
        {
            get => toCustomer;
            set
            {
                toCustomer = value;
                onPropertyChanged("ToCustomer");
            }
        }

        public string Error => Validation.ErorrCheck(this);

        public string this[string columnName] => Validation.PropError(columnName, this);
        public override string ToString()
        {
            return this.ToStringProperties();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}


