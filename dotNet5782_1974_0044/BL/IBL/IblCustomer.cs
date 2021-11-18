using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public interface IblCustomer
    {
        public void AddCustomer(Customer customer);
        public Customer GetCustomer(int id);
        public IEnumerable<CustomerToList> GetCustomers();
        public void UpdateCustomer(int id, string name, string phone);
    }
}
