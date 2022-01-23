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
    public class CustomerAddVM
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
        void Add(object param)
        {
            try
            {
                PLService.AddCustomer(customer);
                DelegateVM.Customer?.Invoke();
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exist");
                customer.Id = null;
            }
         }
        void AddSignIn(object param)
        {
            try
            {
                PLService.AddCustomer(customer);
                DelegateVM.Customer?.Invoke();
                LoginScreen.Id = customer.Id;
                LoginScreen.MyScreen = "CustomerWindow";
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exist");
                customer.Id = null;
            }
        }


    }
}
