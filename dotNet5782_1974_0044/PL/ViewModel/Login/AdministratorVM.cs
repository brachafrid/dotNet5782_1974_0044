using PL.PO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace PL
{
    public class AdministratorVM: NotifyPropertyChangedBase
    {
        public RelayCommand AddDroneToListWindowCommand { get; set; }
        public RelayCommand AddParcelToListWindowCommand { get; set; }
        public RelayCommand AddStationToListWindowCommand { get; set; }
        public RelayCommand AddCustomerToListWindowCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }

        private int selectedTab;

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
            RefreshCommand = new(Tabs.Refresh, null);
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
