using PL.PO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace PL
{
    public  class AdministratorVM
    {
        public  RelayCommand AddDroneToListWindowCommand { get; set; }
        public  RelayCommand AddParcelToListWindowCommand { get; set; }
        public  RelayCommand AddStationToListWindowCommand { get; set; }
        public  RelayCommand AddCustomerToListWindowCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public static IntDependency SelectedTab { get; set; } = new();

        public AdministratorVM()
        {
            AddDroneToListWindowCommand = new(AddDroneToList, null);
            AddParcelToListWindowCommand = new(AddParcelToList, null);
            AddStationToListWindowCommand = new(AddStationToList, null);
            AddCustomerToListWindowCommand = new(AddCustomeroList, null);
            Tabs.changeSelectedTab += changeIndex;
            CloseCommand = new(Close, null);
        }

        public void Close(object param)
        {
            if (param is TabItem tabItem)
            {
                Tabs.CloseTab((tabItem.Header as TabItemFormat).Text);
            }
        }
        public  void AddDroneToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Drones",
                TabContent = "DroneToListWindow",
                Content = new DroneToListVM()
            });
            
        }

        public void changeIndex(int index)
        {
            SelectedTab.Instance = index;
        }
        public  void AddParcelToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Parcels",
                TabContent = "ParcelToListWindow",
                Content = new ParcelToListVM()
            });
            
        }
        public  void AddStationToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Stations",
                TabContent = "StationToListWindow",
                Content = new StationToListVM()
            });
           
        }
        public  void AddCustomeroList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Customers",
                TabContent = "CustomerTolistWindow",
                Content = new StationToListVM()
            });
           
        }
    }
}
