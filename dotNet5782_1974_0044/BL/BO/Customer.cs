using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Customer
    {
        /// <summary>
        /// Customer key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Customer name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Customer phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Customer location
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// IEnumerable of the parcels from the customer
        /// </summary>
        public IEnumerable<ParcelAtCustomer> FromCustomer { get; set; }
        /// <summary>
        /// IEnumerable of the parcels in the way tothe customer
        /// </summary>
        public IEnumerable<ParcelAtCustomer> ToCustomer { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}


