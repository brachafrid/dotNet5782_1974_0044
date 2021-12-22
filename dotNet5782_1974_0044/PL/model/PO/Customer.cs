using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class Customer : INotifyPropertyChanged
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<ParcelAtCustomer> FromCustomer { get; set; }
        public List<ParcelAtCustomer> ToCustomer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


