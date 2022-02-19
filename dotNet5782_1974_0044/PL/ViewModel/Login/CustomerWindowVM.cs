using PL.PO;
using System;
using System.Collections.Generic;
using System.Windows;


namespace PL
{
    public partial class CustomerWindowVM :NotifyPropertyChangedBase
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


        int id;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="Id">id of customer</param>
        public CustomerWindowVM(int Id)
        {
            id = Id;
            Tabs.changeSelectedTab += changeIndex;
            AddParcelCommand = new(AddParcel, null);
            DisplayParcelsFromCommand = new(DisplayParcelsFrom, null);
            DisplayParcelsToCommand = new(DisplayParcelsTo, null);
            DisplayCustomerCommand = new(DisplayCustomer, null);
            LogOutCommand = new(Tabs.LogOut, null);
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


    }
}


