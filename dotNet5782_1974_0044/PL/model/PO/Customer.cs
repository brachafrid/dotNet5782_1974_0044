using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL;


namespace PL.PO
{
    public class Customer : NotifyPropertyChangedBase, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get => id;
            init
            {
                Set(ref id, value);
            }
        }
        private string name;
        [Required(ErrorMessage = "required")]
        public string Name
        {
            get => name;
            set
            {
                Set(ref name, value);
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
                Set(ref phone, value);
            }
        }

        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                Set(ref location, value);
            }
        }
        private List<ParcelAtCustomer> fromCustomer;
        public List<ParcelAtCustomer> FromCustomer
        {
            get => fromCustomer;
            set
            {
                Set(ref fromCustomer, value);
            }
        }
        private List<ParcelAtCustomer> toCustomer;
        public List<ParcelAtCustomer> ToCustomer
        {
            get => toCustomer;
            set
            {
                Set(ref toCustomer, value);
            }
        }

        public string Error => Validation.ErorrCheck(this);

        public string this[string columnName] => Validation.PropError(columnName, this);
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


