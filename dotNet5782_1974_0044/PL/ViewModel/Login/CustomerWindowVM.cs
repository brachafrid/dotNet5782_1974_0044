using PL.PO;
using System;
using System.Collections.Generic;
using System.Windows;


namespace PL
{
    public partial class CustomerWindowVM : GenericList<ParcelAtCustomer>
    {
        private int selectedTab;

        public int SelectedTab
        {
            get { return selectedTab; }
            set { Set(ref selectedTab, value); }
        }


        private Customer customer = new();

        public Customer Customer
        {
            get { return customer; }
            set { Set(ref customer, value); }
        }

        private Visibility visibilityCustomer = Visibility.Collapsed;

        public Visibility VisibilityCustomer
        {
            get { return visibilityCustomer; }
            set => Set(ref visibilityCustomer, value);

        }
        public RelayCommand DisplayParcelsCommand { get; set; }
        public RelayCommand sendParcel { get; set; }
        public RelayCommand collectionParcel { get; set; }
        public RelayCommand gettingParcel { get; set; }
        public RelayCommand AddParcelCommand { get; set; }
        public RelayCommand DisplayParcelsFromCommand { get; set; }
        public RelayCommand DisplayParcelsToCommand { get; set; }
        public RelayCommand DisplayCustomerCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public ParcelAdd parcel { set; get; }
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
            DelegateVM.CustomerChangedEvent += HandleCustomerChanged;
            DelegateVM.ParcelChangedEvent += (sender, e) => Init();
            LogOutCommand = new(Tabs.LogOut, null);
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
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
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
                Content = new ParcelToListVM(id, "From"),
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
                Content = new ParcelToListVM(id, "To"),
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


