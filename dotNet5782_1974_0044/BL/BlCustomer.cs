﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public partial class BL : IBL.IblCustomer
    {
        public void AddCustomer(int id, string name, string phone)
        {
            throw new NotImplementedException();
        }
        public IDAL.DO.Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IDAL.DO.Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            throw new NotImplementedException();
        }
    }
}
