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
        public CustomerAdd customer { get; set; }
        public RelayCommand AddCustomerCommand { get; set; }
        public CustomerAddVM(bool IsSignIn)
        {
            customer = new();
            if (IsSignIn)
                AddCustomerCommand = new(AddSignIn, param => customer.Error == null && customer.Location.Error == null);
            else
                AddCustomerCommand = new(Add, param => customer.Error == null && customer.Location.Error == null);
        }
        private async void Add(object param)
        {
            try
            {
                await PLService.AddCustomer(customer);
                DelegateVM.NotifyCustomerChanged((int)customer.Id);
                Tabs.CloseTab(param as TabItemFormat);
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
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exist");
                customer.Id = null;
            }
        }
        private async void AddSignIn(object param)
        {
            try
            {
                await PLService.AddCustomer(customer);

                DelegateVM.NotifyCustomerChanged((int)customer.Id);
                LoginScreen.Id = customer.Id;
                LoginScreen.MyScreen = "CustomerWindow";
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exist");
                customer.Id = null;
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
        public void Dispose()
        {
        }
    }
}
