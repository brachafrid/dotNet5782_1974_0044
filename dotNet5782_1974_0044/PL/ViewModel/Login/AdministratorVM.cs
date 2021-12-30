using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL.PO;

namespace PL
{
    public class AdministratorVM:DependencyObject
    {
        public static ObservableCollection<TabItemFormat> TabItems { get; set; } = new();
        public RelayCommand AddDroneToListWindowCommand { get; set; }
        public RelayCommand AddParcelToListWindowCommand { get; set; }
        public RelayCommand AddStationToListWindowCommand { get; set; }
        public RelayCommand AddCustomerToListWindowCommand { get; set; }
        public int SelectedTab
        {
            get => (int)GetValue(SelectedTabProperty);
            set =>SetValue(SelectedTabProperty, value); 
        }

        // Using a DependencyProperty as the backing store for SelectedTab.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTabProperty =
            DependencyProperty.Register("SelectedTab", typeof(int), typeof(AdministratorVM), new PropertyMetadata(null));


        public AdministratorVM()
        {
            AddDroneToListWindowCommand = new(AddDroneToList, null);
            AddParcelToListWindowCommand = new(AddParcelToList, null);
            AddStationToListWindowCommand = new(AddStationToList, null);
            AddCustomerToListWindowCommand = new(AddCustomeroList, null);
        }

        public void AddDroneToList(object param)
        {
            var tabItem = TabItems.FirstOrDefault(tab => tab.TabContent == "DroneToListWindow");
            if (tabItem==null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Drones",
                    TabContent = "DroneToListWindow"
                };
                TabItems.Add(newTabItem);
                SelectedTab = TabItems.IndexOf(newTabItem);
            }
            else
            {
               SelectedTab= TabItems.IndexOf(tabItem);
            }
        }
        public void AddParcelToList(object param)
        {
            var tabItem = TabItems.FirstOrDefault(tab => tab.TabContent == "ParcelToListWindow");
            if (tabItem == null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Parcels",
                    TabContent = "ParcelToListWindow"
                };
                TabItems.Add(newTabItem);
                SelectedTab = TabItems.IndexOf(newTabItem);
            }
            else
            {
                SelectedTab = TabItems.IndexOf(tabItem);
            }
        }
        public void AddStationToList(object param)
        {
            var tabItem = TabItems.FirstOrDefault(tab => tab.TabContent == "StationToListWindow");
            if (tabItem == null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Stations",
                    TabContent = "StationToListWindow"
                };
                TabItems.Add(newTabItem);
                SelectedTab = TabItems.IndexOf(newTabItem);
            }
            else
            {
                SelectedTab = TabItems.IndexOf(tabItem);
            }
        }
        public void AddCustomeroList(object param)
        {
            var tabItem = TabItems.FirstOrDefault(tab => tab.TabContent == "CustomerTolistWindow");
            if (tabItem == null)
            {
                var newTabItem = new TabItemFormat()
                {
                    Text = "Customers",
                    TabContent = "CustomerTolistWindow"
                };
                TabItems.Add(newTabItem);
                SelectedTab = TabItems.IndexOf(newTabItem);
            }
            else
            {
                SelectedTab = TabItems.IndexOf(tabItem);
            }
        }
    }
}
