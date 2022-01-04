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
        public CustomerAddVM()
        {
            customer = new();
            AddCustomerCommand = new(Add, param => customer.Error == null&&customer.Location.Error==null);
        }
        public CustomerAddVM(bool IsSignIn)
        {
            customer = new();
            AddCustomerCommand = new(AddSignIn, param => customer.Error == null);
        }
        void Add(object param)
        {
            try
            {
                PLService.AddCustomer(customer);
                DelegateVM.Customer?.Invoke();
                Tabs.CloseTab((param as TabItemFormat).Text);
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
                LoginScreen.MyScreen = "CustomerWindow";
                LoginScreen.Id = customer.Id;
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exist");
                customer.Id = null;
            }
        }


    }
}
