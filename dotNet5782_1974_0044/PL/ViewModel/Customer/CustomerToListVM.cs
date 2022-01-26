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
            sourceList = new ObservableCollection<CustomerToList>();
            list = new ListCollectionView(sourceList);
            UpdateInitList();
            DelegateVM.Customer += UpdateInitList;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        void UpdateInitList()
        {
            sourceList.Clear();
            foreach (var item in PLService.GetCustomers())
                sourceList.Add(item);
        }
   
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                Header = "Customer",
                Content = new CustomerAddVM(false)
            });
        }
    }
}
