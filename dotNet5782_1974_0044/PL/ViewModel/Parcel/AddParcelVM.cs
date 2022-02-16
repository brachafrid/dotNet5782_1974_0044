using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL
{
    public class AddParcelVM : NotifyPropertyChangedBase, IDisposable
    {
        /// <summary>
        /// The Added parcel
        /// </summary>
        public ParcelAdd parcel { set; get; }
        /// <summary>
        /// Command of adding parcel
        /// </summary>
        public RelayCommand AddParcelCommand { get; set; }


        private ObservableCollection<int> customers;
        /// <summary>
        /// ObservableCollection of customers keys
        /// </summary>
        public ObservableCollection<int> Customers
        {
            get => customers;
            set => Set(ref customers, value);
        }

        /// <summary>
        /// Array of piorities
        /// </summary>
        public Array piorities { get; set; }
        /// <summary>
        /// Array of weights
        /// </summary>
        public Array Weight { get; set; }
        /// <summary>
        /// Is administor
        /// </summary>
        public bool IsAdministor { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="isAdministor">is administor</param>
        /// <param name="id">id</param>
        public AddParcelVM(bool isAdministor, int id = 0)
        {
            parcel = new();
            InitCustomersList();
            AddParcelCommand = new(Add, param => parcel.Error == null);
            piorities = Enum.GetValues(typeof(Priorities));
            Weight = Enum.GetValues(typeof(WeightCategories));
            IsAdministor = isAdministor;
            RefreshEvents.ParcelChangedEvent += HandleCustomerListChanged;
            if (!isAdministor)
                parcel.CustomerSender = id;
        }

        /// <summary>
        /// Initializes the customers list
        /// </summary>
        private async void InitCustomersList()
        {
            try
            {
                Customers = new ObservableCollection<int>((await PLService.GetCustomers())
                    .Select(customer => customer.Id));
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Add Parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void HandleCustomerListChanged(object sender, EntityChangedEventArgs e)
        {
            try
            {
                Customers.Clear();
                foreach (var item in (await PLService.GetCustomers()).Select(customer => customer.Id))
                    Customers.Add(item);

            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Add Parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Add parcel
        /// </summary>
        /// <param name="param"></param>
        public async void Add(object param)
        {
            try
            {
                await PLService.AddParcel(parcel);
                RefreshEvents.NotifyParcelChanged();
                if (IsAdministor)
                {
                    RefreshEvents.NotifyCustomerChanged(parcel.CustomerReceives);
                    RefreshEvents.NotifyCustomerChanged(parcel.CustomerSender);
                }
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Add Parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show($"{ex.Id} {ex.Message}", $"Add Parcel", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Add Parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Dispose the eventHandlers
        /// </summary>
        public void Dispose()
        {
            RefreshEvents.ParcelChangedEvent -= HandleCustomerListChanged;
        }
    }
}
