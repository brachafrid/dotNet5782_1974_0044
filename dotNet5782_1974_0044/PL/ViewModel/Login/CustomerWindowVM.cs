using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public partial class CustomerWindowVM : DependencyObject
    {


        public Customer customer
        {
            get { return (Customer)GetValue(customerProperty); }
            set { SetValue(customerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerProperty =
            DependencyProperty.Register("customer", typeof(Customer), typeof(CustomerWindowVM), new PropertyMetadata(new Customer()));


        public RelayCommand DisplayParcelsCommand { get; set; }

        public CustomerWindowVM()
        {
            Init();
            DisplayParcelsCommand = new(DisplayParcels, param => customer.Error == null);
            DelegateVM.Customer += Init;
        }
        public void Init()
        {
           customer = new CustomerHandler().GetCustomer(2);
        }
        public void DisplayParcels(object param)
        {
            new CustomerHandler().GetCustomer(2);
        }
    }
}
