﻿using PL.PO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

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
            try {
                if (e.Id != null)
                {
                    var customer = sourceList.FirstOrDefault(c => c.Id == e.Id);
                    if (customer != default)
                        sourceList.Remove(customer);
                    var newCustomer = PLService.GetCustomers().FirstOrDefault(c => c.Id == e.Id);
                    sourceList.Add(newCustomer);
                }
                else
                {
                    sourceList.Clear();
                    foreach (var item in PLService.GetCustomers())
                        sourceList.Add(item);
                } 
            }
            catch(BO.XMLFileLoadCreateException ex)
            {
                if (ex.Message != string.Empty)
                {
                    MessageBox.Show(ex.Message);
                }
                else
                    MessageBox.Show(ex.ToString());
            }

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
