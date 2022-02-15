using PL.PO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace PL
{
    public class AdministratorVM: NotifyPropertyChangedBase
    {
        /// <summary>
        /// Command of adding drone window
        /// </summary>
        public RelayCommand AddDroneToListWindowCommand { get; set; }
        /// <summary>
        /// Command of adding parcel window
        /// </summary>
        public RelayCommand AddParcelToListWindowCommand { get; set; }
        /// <summary>
        /// Command of adding station window
        /// </summary>
        public RelayCommand AddStationToListWindowCommand { get; set; }
        /// <summary>
        /// Command of adding customer window
        /// </summary>
        public RelayCommand AddCustomerToListWindowCommand { get; set; }
        /// <summary>
        /// Command of refreshing
        /// </summary>
        public RelayCommand RefreshCommand { get; set; }
        /// <summary>
        /// Command of logging out
        /// </summary>
        public RelayCommand LogOutCommand { get; set; }

        private int selectedTab;

        /// <summary>
        /// The selected tab
        /// </summary>
        public int SelectedTab
        {
            get=> selectedTab; 
            set => Set(ref selectedTab, value);
        }

        /// <summary>
        /// constructor
        /// </summary>
        public AdministratorVM()
        {
            AddDroneToListWindowCommand = new(AddDroneToList, null);
            AddParcelToListWindowCommand = new(AddParcelToList, null);
            AddStationToListWindowCommand = new(AddStationToList, null);
            AddCustomerToListWindowCommand = new(AddCustomerToList, null);
            LogOutCommand = new(Tabs.LogOut, null);
            RefreshCommand = new(Refresh, null);
            Tabs.changeSelectedTab += changeIndex;
        }

        /// <summary>
        /// Add tab of drone to list
        /// </summary>
        /// <param name="param"></param>
        public void AddDroneToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Drones",
                Content = new DroneToListVM()
            });
        }

        /// <summary>
        /// refreshing
        /// </summary>
        /// <param name="param"></param>
        public void Refresh(object param)
        {
            DelegateVM.NotifyCustomerChanged();
            DelegateVM.NotifyDroneChanged();
            DelegateVM.NotifyParcelChanged();
            DelegateVM.NotifyStationChanged();
        }

        /// <summary>
        /// change index
        /// </summary>
        /// <param name="index"></param>
        public void changeIndex(int index)
        {
            SelectedTab = index;
        }

        /// <summary>
        /// Add tab of parcel to list
        /// </summary>
        /// <param name="param"></param>
        public void AddParcelToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcels",
                Content = new ParcelToListVM()
            });
        }

        /// <summary>
        /// Add tab of station to list
        /// </summary>
        /// <param name="param"></param>
        public void AddStationToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Stations",
                Content = new StationToListVM()
            });
        }

        /// <summary>
        /// Add tab of customer to list
        /// </summary>
        /// <param name="param"></param>
        public void AddCustomerToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Customers",
                Content = new CustomerToListVM()
            });

        }
    }
}
