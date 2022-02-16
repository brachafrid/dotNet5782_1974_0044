using PL.PO;
using System;
using System.Collections.Generic;
using System.Windows;


namespace PL
{
    public partial class CustomerWindowVM : GenericList<ParcelAtCustomer>
    {
        private int selectedTab;

        /// <summary>
        /// The selected tab
        /// </summary>
        public int SelectedTab
        {
            get { return selectedTab; }
            set { Set(ref selectedTab, value); }
        }

        private Customer customer = new();

        /// <summary>
        /// customer
        /// </summary>
        public Customer Customer
        {
            get { return customer; }
            set { Set(ref customer, value); }
        }

        /// <summary>
        /// Command of displaing parcels
        /// </summary>
        public RelayCommand DisplayParcelsCommand { get; set; }
        /// <summary>
        /// Command of sending parcel
        /// </summary>
        public RelayCommand sendParcel { get; set; }
        /// <summary>
        /// Command of collection parcel
        /// </summary>
        public RelayCommand collectionParcel { get; set; }
        /// <summary>
        /// Command of getting parcel
        /// </summary>
        public RelayCommand gettingParcel { get; set; }
        /// <summary>
        /// Command of adding parcel
        /// </summary>
        public RelayCommand AddParcelCommand { get; set; }
        /// <summary>
        /// Command of parcels from customer
        /// </summary>
        public RelayCommand DisplayParcelsFromCommand { get; set; }
        /// <summary>
        /// Command of displaing parcels to customer
        /// </summary>
        public RelayCommand DisplayParcelsToCommand { get; set; }
        /// <summary>
        /// Command of displaing customer
        /// </summary>
        public RelayCommand DisplayCustomerCommand { get; set; }
        /// <summary>
        /// Command of logging out
        /// </summary>
        public RelayCommand LogOutCommand { get; set; }

        /// <summary>
        /// The added parcel
        /// </summary>
        public ParcelAdd parcel { set; get; }
        public RelayCommand RefreshCommand { get; set; }

        int id;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="Id">id of customer</param>
        public CustomerWindowVM(int Id)
        {
            id = Id;
            Init();
            Tabs.changeSelectedTab += changeIndex;
            AddParcelCommand = new(AddParcel, null);
            DisplayParcelsFromCommand = new(DisplayParcelsFrom, null);
            DisplayParcelsToCommand = new(DisplayParcelsTo, null);
            DisplayCustomerCommand = new(DisplayCustomer, null);
            RefreshEvents.CustomerChangedEvent += HandleCustomerChanged;
            RefreshEvents.ParcelChangedEvent += (sender, e) => Init();
            LogOutCommand = new(Tabs.LogOut, null);
            RefreshCommand = new(Tabs.Refresh, null);
        }

        /// <summary>
        /// Handle customer changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void HandleCustomerChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                Init();
        }

        /// <summary>
        /// Initializes the customer
        /// </summary>
        public async void Init()
        {
            try
            {
                customer = await PLService.GetCustomer(id);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Login Customer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Login Customer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Change index
        /// </summary>
        /// <param name="index">index</param>
        public void changeIndex(int index)
        {
            SelectedTab = index;
        }

        /// <summary>
        /// Display customer
        /// </summary>
        /// <param name="param"></param>
        public void DisplayCustomer(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Customer",
                Content = new UpdateCustomerVM(id, false),
            });
        }

        /// <summary>
        /// Add parcel
        /// </summary>
        /// <param name="param"></param>
        public void AddParcel(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Add Parcel",
                Content = new AddParcelVM(false, id),
            });
        }

        /// <summary>
        /// Display parcels from customer
        /// </summary>
        /// <param name="param"></param>
        public void DisplayParcelsFrom(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcels From Customer",
                Content = new ParcelToListVM(id, ParcelListWindowState.FROM_CUSTOMER),
            });
        }

        /// <summary>
        /// Display parcels to customer
        /// </summary>
        /// <param name="param"></param>
        public void DisplayParcelsTo(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcels To Customer",
                Content = new ParcelToListVM(id, ParcelListWindowState.TO_CUSTOMER),
            });
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="param"></param>
        public override void AddEntity(object param)
        {
            throw new NotImplementedException();
        }
    }
}


