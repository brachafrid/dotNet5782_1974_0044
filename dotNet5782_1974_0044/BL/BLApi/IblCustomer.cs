
using System.Collections.Generic;

using BO;

namespace BLApi
{
    public interface IBlCustomer
    {
        public void AddCustomer(Customer customer);
        public Customer GetCustomer(int id);
        public IEnumerable<CustomerToList> GetCustomers();
        public IEnumerable<CustomerToList> GetActiveCustomers();
        public void UpdateCustomer(int id, string name, string phone);
        public void DeleteCustomer(int id);
        public bool IsActiveCustomer(int id);
    }
}

