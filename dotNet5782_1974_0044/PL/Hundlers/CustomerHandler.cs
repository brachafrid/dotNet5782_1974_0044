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
            ibal.AddCustomer(ConvertBackCustomer(customer));
        }
        public Customer GetCustomer(int id)
        {
            //BO.Customer customerBL = ibal.GetCustomer(id);
            return ConvertCustomer(ibal.GetCustomer(id));
        }
        public IEnumerable<CustomerToList> GetCustomers()
        {
            return ibal.GetCustomers().Select(customer => ConvertCustomerToList(customer));
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            ibal.UpdateCustomer(id, name, phone);
        }
        public PO.Customer ConvertCustomer(BO.Customer customer)
        {
            return new PO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone, 

                //Location = new BO.Location { Latitude = customer.Location.Latitude, Longitude = customer.Location.Longitude},
                //FromCustomer = customer.FromCustomer,
                //ToCustomer = customer.ToCustomer

            };
        }
        public BO.Customer ConvertBackCustomer(PO.Customer customer)
        {
            return new BO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                //Location = new PO.Location { Latitude = customer.Location.Latitude, Longitude = customer.Location.Longitude },
                //FromCustomer = customer.FromCustomer,
                //ToCustomer = customer.ToCustomer

            };
        }

        public PO.CustomerToList ConvertCustomerToList(BO.CustomerToList customerToList)
        {
            return new PO.CustomerToList
            {
                Id = customerToList.Id,
                Name = customerToList.Name,
                Phone = customerToList.Phone,

                //Location = new BO.Location { Latitude = customer.Location.Latitude, Longitude = customer.Location.Longitude},
                //FromCustomer = customer.FromCustomer,
                //ToCustomer = customer.ToCustomer

            };
        }
        public BO.CustomerToList ConvertBackCustomerToList(PO.CustomerToList customerToList)
        {
            return new BO.CustomerToList
            {
                Id = customerToList.Id,
                Name = customerToList.Name,
                Phone = customerToList.Phone,
                //Location = new PO.Location { Latitude = customerToList.Location.Latitude, Longitude = customerToList.Location.Longitude },
                //FromCustomer = customer.FromCustomer,
                //ToCustomer = customer.ToCustomer

            };
        }
    }
}
