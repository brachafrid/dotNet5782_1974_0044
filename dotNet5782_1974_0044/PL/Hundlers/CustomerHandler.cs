using BLApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Hundlers
{
    class CustomerHandler
    {
        private IBL ibal  = BLFactory.GetBL();

        public void AddCustomer(Customer customer)
        {
            ibal.AddCustomer(ConvertCustomer(customer));
        }
        public Customer GetCustomer(int id)
        {
            BO.Customer customerBL = ibal.GetCustomer(id);
            ConvertCustomer(customerBL);
        }
        public IEnumerable<CustomerToList> GetCustomers()
        {

        }
        public void UpdateCustomer(int id, string name, string phone)
        {

        }
        public PO.Customer ConvertCustomer(BO.Customer customer)
        {
            return new PO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = new PO.Location { Latitude = customer.Location.Latitude, Longitude = customer.Location.Longitude},
                FromCustomer = customer.FromCustomer,
                ToCustomer = customer.ToCustomer

            };
        }
    }
}
