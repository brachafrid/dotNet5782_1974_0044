using PL.PO;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PL
{
    class UpdateCustomerVM : DependencyObject
    {
        private int id;
        public RelayCommand OpenParcelCommand { get; set; }
        public Customer customer
        {
            get { return (Customer)GetValue(customerProperty); }
            set { SetValue(customerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerProperty =
            DependencyProperty.Register("customer", typeof(Customer), typeof(UpdateCustomerVM), new PropertyMetadata(new Customer()));

        public string customerName
        {
            get { return (string)GetValue(customerNameProperty); }
            set { SetValue(customerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerNameProperty =
            DependencyProperty.Register("customerName", typeof(string), typeof(UpdateCustomerVM), new PropertyMetadata(""));
        //public Visble ListsVisble { get; set; }
        public bool IsAdministor { get; set; }
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

        public UpdateCustomerVM(int id, bool isAdministor)
        {
            IsAdministor = isAdministor;
            this.id = id;
            InitThisCustomer();
            UpdateCustomerCommand = new(UpdateCustomer, param => customer?.Error == null);
            DeleteCustomerCommand = new(DeleteCustomer, param => customer?.Error == null);
            DelegateVM.CustomerChangedEvent += HandleACustomerChanged;
            OpenParcelCommand = new(Tabs.OpenDetailes, null);
            IsAdministor = isAdministor;
        }
        private void HandleACustomerChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitThisCustomer();
        }
        public async void InitThisCustomer()
        {
            try
            {
                customer = await PLService.GetCustomer(id);
                customerName = customer.Name;
                customerPhone = customer.Phone;
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }

        public async void UpdateCustomer(object param)
        {
            try
            {
                if (customerName != customer.Name || customerPhone != customer.Phone)
                {
                    await PLService.UpdateCustomer(customer.Id, customer.Name, customer.Phone);
                    customerName = customer.Name;
                    customerPhone = customer.Phone;
                    DelegateVM.NotifyCustomerChanged();

                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }

        }
        public void DeleteCustomer(object param)
        {
            try
            {
                if (MessageBox.Show("You're sure you want to delete this customer?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    PLService.DeleteCustomer(customer.Id);
                    MessageBox.Show("The customer was successfully deleted");

                    if (!IsAdministor)
                    {
                        LoginScreen.MyScreen = "LoginWindow";
                        Tabs.TabItems.Clear();
                        DelegateVM.Reset();
                    }
                    else
                    {
                        DelegateVM.CustomerChangedEvent -= HandleACustomerChanged;
                        DelegateVM.NotifyCustomerChanged(customer.Id);
                        Tabs.CloseTab(param as TabItemFormat);
                    }

                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
    }
}

