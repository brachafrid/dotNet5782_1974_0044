using PL.PO;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    class UpdateCustomerVM : NotifyPropertyChangedBase, IDisposable
    {
        private int id;
        /// <summary>
        /// command of opening parcel
        /// </summary>
        public RelayCommand OpenParcelCommand { get; set; }
        private Customer customer;

        /// <summary>
        /// customer
        /// </summary>
        public Customer Customer
        {
            get { return customer; }
            set {
                Set(ref customer, value);
                customer = value;
            }
        }
        private string customerName;

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                Set(ref customerName, value);
            }
        }
        private string customerPhone;

        /// <summary>
        /// The phone of the customer
        /// </summary>
        public string CustomerPhone
        {
            get { return customerPhone; }
            set {
                Set(ref customerPhone, value);
            }
        }

        //public Visble ListsVisble { get; set; }
        public bool IsAdministor { get; set; }

        /// <summary>
        /// command of updating customer
        /// </summary>
        public RelayCommand UpdateCustomerCommand { get; set; }
        /// <summary>
        /// command of deleting customer
        /// </summary>
        public RelayCommand DeleteCustomerCommand { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <param name="isAdministor">is administor</param>
        public UpdateCustomerVM(int id, bool isAdministor)
        {
            IsAdministor = isAdministor;
            this.id = id;
            InitThisCustomer();
            UpdateCustomerCommand = new(UpdateCustomer, param => customer?.Error == null);
            DeleteCustomerCommand = new(DeleteCustomer, param => customer?.Error == null);
            RefreshEvents.CustomerChangedEvent += HandleACustomerChanged;
            OpenParcelCommand = new(Tabs.OpenDetailes, null);
        }

        /// <summary>
        /// Handle customer changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void HandleACustomerChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitThisCustomer();
        }

        /// <summary>
        /// Initialize this customer
        /// </summary>
        public async void InitThisCustomer()
        {
            try
            {
                Customer = await PLService.GetCustomer(id);
                customerName = customer.Name;
                customerPhone = customer.Phone;
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message,$"Init Customer Id:{id}",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Init Customer Id:{id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="param"></param>
        public async void UpdateCustomer(object param)
        {
            try
            {
                if (customerName != Customer.Name || customerPhone != Customer.Phone)
                {
                    await PLService.UpdateCustomer(customer.Id, customer.Name, customer.Phone);
                    customerName = customer.Name;
                    customerPhone = customer.Phone;
                    RefreshEvents.NotifyCustomerChanged(Customer.Id);
                    RefreshEvents.NotifyParcelChanged();

                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, $"Update Customer Id:{id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Update Customer Id:{id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Update Customer Id:{id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="param"></param>
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
                        LoginScreen.MyScreen = Screen.LOGIN;
                        Tabs.TabItems.Clear();
                        RefreshEvents.Reset();
                    }
                    else
                    {
                        RefreshEvents.CustomerChangedEvent -= HandleACustomerChanged;
                        RefreshEvents.NotifyCustomerChanged(Customer.Id);
                        Tabs.CloseTab(param as TabItemFormat);
                    }

                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Delete Customer Id:{id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Delete Customer Id:{id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Dispose()
        {
            RefreshEvents.CustomerChangedEvent -= HandleACustomerChanged;
        }
    }
}

