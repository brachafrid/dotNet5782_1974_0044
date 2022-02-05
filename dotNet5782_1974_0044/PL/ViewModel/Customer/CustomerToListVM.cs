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
            sourceList = new ObservableCollection<CustomerToList>(PLService.GetCustomers());
            list = new ListCollectionView(sourceList);
            DelegateVM.CustomerChangedEvent += HandleCustomerChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        private void HandleCustomerChanged(object sender, EntityChangedEventArgs e)
        {
            var customer = sourceList.FirstOrDefault(c => c.Id == e.Id);
            if (customer != default)
                sourceList.Remove(customer);
            var newCustomer = PLService.GetCustomers().FirstOrDefault(c=> c.Id == e.Id);
            sourceList.Add(newCustomer);
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
