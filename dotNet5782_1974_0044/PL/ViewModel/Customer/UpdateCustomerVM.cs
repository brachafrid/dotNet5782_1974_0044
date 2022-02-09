using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    class UpdateCustomerVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
        private int id;
        public RelayCommand OpenParcelCommand { get; set; }
        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set { customer = value;
                onPropertyChanged("Customer");
            }
        }
        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                onPropertyChanged("CustomerName");
            }
        }
        private string customerPhone;

        public string CustomerPhone
        {
            get { return customerPhone; }
            set { customerPhone = value;
                onPropertyChanged("CustomerPhone");
            }
        }

        //public Visble ListsVisble { get; set; }
        public bool IsAdministor { get; set; }

        public RelayCommand UpdateCustomerCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }

        //public RelayCommand TryCommand { get; set; }

        public UpdateCustomerVM(int id, bool isAdministor)
        {
            IsAdministor = isAdministor;
            this.id = id;
            InitThisCustomer();
            customerName = Customer.Name;
            customerPhone = Customer.Phone;
            UpdateCustomerCommand = new(UpdateCustomer, param => Customer.Error == null);
            DeleteCustomerCommand = new(DeleteCustomer, param => Customer.Error == null);
            DelegateVM.CustomerChangedEvent += HandleACustomerChanged;
            OpenParcelCommand = new(Tabs.OpenDetailes, null);
            IsAdministor = isAdministor;
        }
        private void HandleACustomerChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitThisCustomer();
        }
        public void InitThisCustomer()
        {
            try
            {
                customer = PLService.GetCustomer(id);
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

        public void UpdateCustomer(object param)
        {
            try
            {
                if (customerName != Customer.Name || customerPhone != Customer.Phone)
                {
                    PLService.UpdateCustomer(Customer.Id, Customer.Name, Customer.Phone);
                    customerName = Customer.Name;
                    customerPhone = Customer.Phone;
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
                    PLService.DeleteCustomer(Customer.Id);
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
                        //DelegateVM.NotifyCustomerChanged(Customer.Id);
                        DelegateVM.NotifyCustomerChanged();
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

