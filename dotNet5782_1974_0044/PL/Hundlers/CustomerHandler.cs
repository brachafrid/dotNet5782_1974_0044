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
            //BO.Customer customerBL = ibal.GetCustomer(id);
            return ConvertBackCustomer(ibal.GetCustomer(id));
        }
        public IEnumerable<CustomerToList> GetCustomers()
        {
           return ibal.GetCustomers();
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            ibal.UpdateCustomer(id, name, phone);
        }
        public BO.Customer ConvertCustomer(PO.Customer customer)
        {
            return new BO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = new BO.Location { Latitude = customer.Location.Latitude, Longitude = customer.Location.Longitude},
                FromCustomer = customer.FromCustomer,
                ToCustomer = customer.ToCustomer

            };
        }
        public PO.Customer ConvertBackCustomer(BO.Customer customer)
        {
            return new PL.PO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = new PO.Location { Latitude = customer.Location.Latitude, Longitude = customer.Location.Longitude },
                FromCustomer = customer.FromCustomer,
                ToCustomer = customer.ToCustomer

            };
        }
    }
}
