using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IblCustomer
    {
        public void AddCustomer(BO.Customer customer);
        public BO.Customer GetCustomer(int id);
        public IEnumerable<BO.CustomerToList> GetCustomers();
        public void UpdateCustomer(int id, string name, string phone);
    }
}
