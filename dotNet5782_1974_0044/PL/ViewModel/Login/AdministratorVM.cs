using PL.PO;
using System.Collections.ObjectModel;
using System.Linq;

namespace PL
{
    public  class AdministratorVM
    {
        public  RelayCommand AddDroneToListWindowCommand { get; set; }
        public  RelayCommand AddParcelToListWindowCommand { get; set; }
        public  RelayCommand AddStationToListWindowCommand { get; set; }
        public  RelayCommand AddCustomerToListWindowCommand { get; set; }
        public  IntDependency SelectedTab { get; set; } = new();

        public AdministratorVM()
        {
            AddDroneToListWindowCommand = new(AddDroneToList, null);
            AddParcelToListWindowCommand = new(AddParcelToList, null);
            AddStationToListWindowCommand = new(AddStationToList, null);
            AddCustomerToListWindowCommand = new(AddCustomeroList, null);
        }

        public  void AddDroneToList(object param)
        {
            var tabItem = Tabs.TabItems.FirstOrDefault(tab => tab.TabContent == "DroneToListWindow");
            if (tabItem == null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Drones",
                    TabContent = "DroneToListWindow"
                };
                Tabs.TabItems.Add(newTabItem);
                SelectedTab.Instance = Tabs.TabItems.IndexOf(newTabItem);
            }
            else
            {
                SelectedTab.Instance = Tabs.TabItems.IndexOf(tabItem);
            }
        }
        public  void AddParcelToList(object param)
        {
            var tabItem = Tabs.TabItems.FirstOrDefault(tab => tab.TabContent == "ParcelToListWindow");
            if (tabItem == null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Parcels",
                    TabContent = "ParcelToListWindow"
                };
                Tabs.TabItems.Add(newTabItem);
                SelectedTab.Instance = Tabs.TabItems.IndexOf(newTabItem);
            }
            else
            {
                SelectedTab.Instance = Tabs.TabItems.IndexOf(tabItem);
            }
        }
        public  void AddStationToList(object param)
        {
            var tabItem = Tabs.TabItems.FirstOrDefault(tab => tab.TabContent == "StationToListWindow");
            if (tabItem == null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Stations",
                    TabContent = "StationToListWindow"
                };
                Tabs.TabItems.Add(newTabItem);
                SelectedTab.Instance = Tabs.TabItems.IndexOf(newTabItem);
            }
            else
            {
                SelectedTab.Instance = Tabs.TabItems.IndexOf(tabItem);
            }
        }
        public  void AddCustomeroList(object param)
        {
            var tabItem = Tabs.TabItems.FirstOrDefault(tab => tab.TabContent == "CustomerTolistWindow");
            if (tabItem == null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Customers",
                    TabContent = "CustomerTolistWindow"
                };
                Tabs.TabItems.Add(newTabItem);
                SelectedTab.Instance = Tabs.TabItems.IndexOf(newTabItem);
            }
            else
            {
                SelectedTab.Instance = Tabs.TabItems.IndexOf(tabItem);
            }
        }
    }
}
