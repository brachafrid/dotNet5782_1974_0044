﻿using PL.PO;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    class UpdateCustomerVM : NotifyPropertyChangedBase
    {
        private int id;
        public RelayCommand OpenParcelCommand { get; set; }
        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set {
                Set(ref customer, value);
                customer = value;
            }
        }
        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set
            {
                Set(ref customerName, value);
            }
        }
        private string customerPhone;

        public string CustomerPhone
        {
            get { return customerPhone; }
            set {
                Set(ref customerPhone, value);
            }
        }

        //public Visble ListsVisble { get; set; }
        public bool IsAdministor { get; set; }

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
                if (customerName != Customer.Name || customerPhone != Customer.Phone)
                {
                    await PLService.UpdateCustomer(customer.Id, customer.Name, customer.Phone);
                    customerName = customer.Name;
                    customerPhone = customer.Phone;
                    DelegateVM.NotifyCustomerChanged(Customer.Id);

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
        public async void DeleteCustomer(object param)
        {
            try
            {
                if (MessageBox.Show("You're sure you want to delete this customer?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                   await PLService.DeleteCustomer(Customer.Id);
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
                        DelegateVM.NotifyCustomerChanged(Customer.Id);
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

