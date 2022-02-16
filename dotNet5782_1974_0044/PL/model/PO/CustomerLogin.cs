using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
  public  class CustomerLogin: NotifyPropertyChangedBase, IDataErrorInfo
    {
        private int? id;
        /// <summary>
        /// customer login key
        /// </summary>
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Id
        {
            get => id;
            set => Set(ref id, value);
        }
        private string phone;

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public string Phone
        {
            get { return phone; }
            set { Set(ref phone, value); }
        }

        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}
