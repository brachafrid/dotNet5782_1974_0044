using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class CustomerToListVM : GenericList<CustomerToList>, IDisposable
    {
        /// <summary>
        /// constructor
        /// </summary>
        public CustomerToListVM()
        {
            InitList();
            DelegateVM.CustomerChangedEvent += HandleCustomerChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }

        /// <summary>
        /// Initialize a List
        /// </summary>
        private async void InitList()
        {
            sourceList = new ObservableCollection<CustomerToList>(await PLService.GetCustomers());
            list = new ListCollectionView(sourceList);
        }

        /// <summary>
        /// Handle customer changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private async void HandleCustomerChanged(object sender, EntityChangedEventArgs e)
        {
            try
            {
                if (e.Id != null)
                {
                    var customer = sourceList.FirstOrDefault(c => c.Id == e.Id);
                    if (customer != default)
                        sourceList.Remove(customer);

                    var newCustomer = (await PLService.GetCustomers()).FirstOrDefault(c => c.Id == e.Id);
                    sourceList.Add(newCustomer);
                }
                else
                {
                    sourceList.Clear();
                    foreach (var item in await PLService.GetCustomers())
                        sourceList.Add(item);
                }
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                if (ex.Message != string.Empty)
                {
                    MessageBox.Show(ex.Message);
                }
                else
                    MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="param"></param>
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                Header = "Customer",
                Content = new CustomerAddVM(false)
            });
        }

        /// <summary>
        /// Dispose all the eventHundlers
        /// </summary>
        public void Dispose()
        {
            DelegateVM.CustomerChangedEvent -= HandleCustomerChanged;
        }
    }
}
