using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Customer
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public IEnumerable<ParcelAtCustomer> FromCustomer { get; set; }
        public IEnumerable<ParcelAtCustomer> ToCustomer { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}


