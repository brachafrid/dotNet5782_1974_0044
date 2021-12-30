using BO;
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

        public PL.PO.Customer customer
        {
            get { return (PL.PO.Customer)GetValue(customerProperty); }
            set { SetValue(customerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerProperty =
            DependencyProperty.Register("customer", typeof(PL.PO.Customer), typeof(UpdateCustomerVM), new PropertyMetadata(new PL.PO.Customer()));

        public string customerName
        {
            get { return (string)GetValue(customerNameProperty); }
            set { SetValue(customerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerNameProperty =
            DependencyProperty.Register("customerName", typeof(string), typeof(UpdateCustomerVM), new PropertyMetadata(""));



        public string customerPhone
        {
            get { return (string)GetValue(customerPhoneProperty); }
            set { SetValue(customerPhoneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customerPhone.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerPhoneProperty =
            DependencyProperty.Register("customerPhone", typeof(string), typeof(UpdateCustomerVM), new PropertyMetadata(""));


        public RelayCommand UpdateCustomerCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        
        //public RelayCommand TryCommand { get; set; }

        public UpdateCustomerVM()
        {
            init();
            customerName = customer.Name;                  
            customerPhone = customer.Phone;
            UpdateCustomerCommand = new(UpdateCustomer, param => customer.Error == null);
            DeleteCustomerCommand = new(DeleteCustomer, param => customer.Error == null);
            //TryCommand = new(TryCommandFunc, param => customer.Error == null);
            DelegateVM.Customer += init;
        }
        public void init()
        {
            customer = new CustomerHandler().GetCustomer(2);
        }
        
        public void UpdateCustomer(object param)
        {
            if(customerName != customer.Name || customerPhone != customer.Phone)
            {
                new CustomerHandler().UpdateCustomer(customer.Id, customer.Name, customer.Phone);
                customerName = customer.Name;
                customerPhone = customer.Phone;
            }
        }

        public void DeleteCustomer(object param)
        {
            try
            {
                 if (MessageBox.Show("You're sure you want to delete this customer?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                 {
                    new CustomerHandler().DeleteCustomer(customer.Id);
                    MessageBox.Show("The customer was successfully deleted");
                 }
            }
           
            catch (ThereAreAssociatedOrgansException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        //public void TryCommandFunc(object param)
        //{
        //    MessageBox.Show("double click");
        //}

    }
}
