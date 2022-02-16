using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int? Id
        {
            get => id;
            set => Set(ref id, value);
        }
        private string? phone;

        public string? Phone
        {
            get { return phone; }
            set { Set(ref phone, value); }
        }

        public string Error
        {
            get
            {
                foreach (var item in GetType().GetProperties())
                {
                    if (this[item.Name] != null)
                        return "invalid" + item.Name;
                }
                return
                    null;
            }
        }

        public string this[string columnName]
        {

            get => null;

        }
    }
}
