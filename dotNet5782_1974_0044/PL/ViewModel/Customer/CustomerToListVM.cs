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
       public RelayCommand DoubleClick { set; get; }
        public CustomerToListVM()
        {            
            list = new ListCollectionView(new CustomerHandler().GetCustomers().ToList());
            DoubleClick = new(OpenDetails, null);
        }
      public void OpenDetails(object id)
        {
            Tabs.TabItems.Add(new()
            {
                TabContent = "UpdateCustomerView",
                Text = "customer " + id
            }); 
        }
    }
}
