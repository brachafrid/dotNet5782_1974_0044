using BLApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
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
                Location = LocationHandler.ConvertLocation(customer.Location),
                FromCustomer = customer.FromCustomer.Select(item => ParcelAtCustomerHandler.ConvertParcelAtCustomer(item)).ToList(),
                ToCustomer = customer.ToCustomer.Select(item => ParcelAtCustomerHandler.ConvertParcelAtCustomer(item)).ToList()
            };
        }
        public BO.Customer ConvertBackCustomer(PO.Customer customer)
        {
            return new BO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = LocationHandler.ConvertBackLocation(customer.Location),
                FromCustomer = customer.FromCustomer.Select(item => ParcelAtCustomerHandler.ConvertBackParcelAtCustomer(item)).ToList(),
                ToCustomer = customer.ToCustomer.Select(item => ParcelAtCustomerHandler.ConvertBackParcelAtCustomer(item)).ToList()
            };
        }

        public PO.CustomerToList ConvertCustomerToList(BO.CustomerToList customerToList)
        {
            return new PO.CustomerToList
            {
                Id = customerToList.Id,
                Name = customerToList.Name,
                Phone = customerToList.Phone,
                NumParcelSentDelivered = customerToList.NumParcelSentDelivered,
                NumParcelSentNotDelivered = customerToList.NumParcelSentNotDelivered,
                NumParcelReceived = customerToList.NumParcelReceived,
                NumParcelWayToCustomer = customerToList.NumParcelWayToCustomer
            };
        }

        public BO.CustomerToList ConvertBackCustomerToList(PO.CustomerToList customerToList)
        {
            return new BO.CustomerToList
            {
                Id = customerToList.Id,
                Name = customerToList.Name,
                Phone = customerToList.Phone,
                NumParcelSentDelivered = customerToList.NumParcelSentDelivered,
                NumParcelSentNotDelivered = customerToList.NumParcelSentNotDelivered,
                NumParcelReceived = customerToList.NumParcelReceived,
                NumParcelWayToCustomer = customerToList.NumParcelWayToCustomer
            };
        }
    }
}
