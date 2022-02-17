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
            RefreshEvents.CustomerChangedEvent += HandleCustomerChanged;
        }

        /// <summary>
        /// Initialize a List
        /// </summary>
        private async void InitList()
        {
            try
            {
                sourceList = new ObservableCollection<CustomerToList>(await PLService.GetCustomers());
                List = new ListCollectionView(sourceList);
                Count = (uint)List.Count;
                if (List.Count == 0)
                    Count = 0;
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Init Custoners List", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
                    {
                        sourceList.Remove(customer);
                        var newCustomer = (await PLService.GetCustomers()).FirstOrDefault(c => c.Id == e.Id);
                        if (newCustomer != null)
                            sourceList.Add(newCustomer);
                    }

                }
                else
                {
                    sourceList.Clear();
                    foreach (var item in await PLService.GetCustomers())
                        sourceList.Add(item);
                }
                Count = (uint)List.Count;
                if (List.Count == 0)
                    Count = 0;
                MessageBox.Show(Count.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Changed Customer", MessageBoxButton.OK, MessageBoxImage.Error);
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
            RefreshEvents.CustomerChangedEvent -= HandleCustomerChanged;
        }
    }
}
