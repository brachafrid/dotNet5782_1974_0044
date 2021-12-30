using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PL.PO;
using System.Collections.ObjectModel;

namespace PL
{
   public class CustomerToListVM:GenericList<CustomerToList>
    {
        public CustomerToListVM()
        {            
            list = new ListCollectionView(new CustomerHandler().GetCustomers().ToList());
        }
    }
}
