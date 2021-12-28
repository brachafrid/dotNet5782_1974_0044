using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    class UpdateCustomerVM : DependencyObject
    {


        public Customer customer
        {
            get { return (Customer)GetValue(customerProperty); }
            set { SetValue(customerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerProperty =
            DependencyProperty.Register("customer", typeof(Customer), typeof(UpdateCustomerVM), new PropertyMetadata(new Customer()));

        public RelayCommand UpdateCustomerCommand { get; set; }

        public UpdateCustomerVM()
        {
            init();
            UpdateCustomerCommand = new(UpdateCustomer, param => customer.Error == null);
            DelegateVM.Customer += init;
        }
        public void init()
        {
            customer = new CustomerHandler().GetCustomer(2);
        }
        public void UpdateCustomer(object param)
        {
            new CustomerHandler().UpdateCustomer(customer.Id, customer.Name, customer.Phone);
        }

    }
}
