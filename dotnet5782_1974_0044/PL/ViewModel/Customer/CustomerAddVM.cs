using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PL.PO;


namespace PL
{
    public class CustomerAddVM : IDisposable
    {
        /// <summary>
        /// The added customer
        /// </summary>
        public CustomerAdd customer { get; set; }
        /// <summary>
        /// Command of adding customer
        /// </summary>
        public RelayCommand AddCustomerCommand { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="IsSignIn">Is sign in</param>
        public CustomerAddVM(bool IsSignIn)
        {
            customer = new();
            if (IsSignIn)
                AddCustomerCommand = new(AddSignIn, param => customer.Error == null && customer.Location.Error == null);
            else
                AddCustomerCommand = new(Add, param => customer.Error == null && customer.Location.Error == null);
        }

        /// <summary>
        /// Add customer
        /// </summary>
        /// <param name="param">tab customer</param>
        private async void Add(object param)
        {
            try
            {
                await PLService.AddCustomer(customer);
                DelegateVM.NotifyCustomerChanged((int)customer.Id);
                DelegateVM.NotifyParcelChanged();
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Adding Customer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show( ex.Message + Environment.NewLine+$"The Id {ex.Id}", "Adding Customer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                customer.Id = null;
            }
        }

        /// <summary>
        /// Add sign in
        /// </summary>
        /// <param name="param"></param>
        private async void AddSignIn(object param)
        {
            try
            {
                await PLService.AddCustomer(customer);
                DelegateVM.NotifyCustomerChanged((int)customer.Id);
                LoginScreen.Id = customer.Id;
                LoginScreen.MyScreen = Screen.CUSTOMER;
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show((ex.Message!=string.Empty?ex.Message:ex.ToString()) + Environment.NewLine, "Adding Customer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                customer.Id = null;
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Adding Customer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Dispose()
        {
        }
    }
}
