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
    public class CustomerToListVM : GenericList<CustomerToList>
    {

        public CustomerToListVM()
        {
            UpdateInitList();
            DelegateVM.Customer += UpdateInitList;
            DoubleClick = new(OpenDetails, null);
        }
        void UpdateInitList()
        {
            list = new ListCollectionView( PLService.GetCustomers().ToList());
        }
      public void OpenDetails(object param)
        {
            if(param != null)
            Tabs.AddTab(new()
            {
                TabContent = "UpdateCustomerView",
                Text = "customer " + (param as CustomerToList).Id,
                Id = (param as CustomerToList).Id,
                Content = new UpdateCustomerVM((param as CustomerToList).Id)
            }); 
        }
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                TabContent = "AddCustomerView",
                Text = "Customer",
                Content = new CustomerAddVM(false)
            });
        }
    }
}
