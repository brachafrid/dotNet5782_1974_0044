using System;
using System.Collections.Generic;
using DO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLApi
{
  public  interface IDalCustomer
    {
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude);
        public void UpdateCustomer(Customer customer, string name,string phone);
        public Customer GetCustomer(int id);
        public IEnumerable<Customer> GetCustomers();
        public void DeleteCustomer(int id);
    }
}
