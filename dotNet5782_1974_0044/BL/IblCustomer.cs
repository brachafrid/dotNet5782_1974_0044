using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
   public interface IblCustomer
    {
        public void AddCustomer(int id, string name, string phone);
        public IDAL.DO.Customer GetCustomer(int id);
        public IEnumerable<IDAL.DO.Customer> GetCustomers();
        public void UpdateCustomer(int id, string name, string phone);
    }
}
