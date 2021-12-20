﻿
using System.Collections.Generic;

using BL.BO;
namespace BL
{
    namespace BLApi
    {
        public interface IBlCustomer
        {
            public void AddCustomer(Customer customer);
            public Customer GetCustomer(int id);
            public IEnumerable<CustomerToList> GetCustomers();
            public void UpdateCustomer(int id, string name, string phone);
        }
    }

}
