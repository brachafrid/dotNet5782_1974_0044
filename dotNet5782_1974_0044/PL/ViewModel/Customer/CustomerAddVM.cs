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
            AddCustomerCommand = new(Add, param => customer.Error == null);
            DelegateVM.Customer();
        }
        void Add(object param)
        {
            try
            {
                PLService.AddCustomer(customer);
                Tabs.CloseTab((param as TabItemFormat).Text);

            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exist");
                customer.Id = null;
            }
        }


    }
}
