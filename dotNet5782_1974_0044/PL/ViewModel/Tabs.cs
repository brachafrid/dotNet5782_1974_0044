using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public static class Tabs
    {
        public static ObservableCollection<TabItemFormat> TabItems { get; set; } = new();
        public static RelayCommand CloseCommandTab { get; set; }
        public static Action<int> changeSelectedTab;
        static Tabs()
        {
            CloseCommandTab = new(CloseTab, null);
        }
        public static void CloseTab(object param)
        {
            if(param is TabItemFormat tabItem)
            {
                TabItems.Remove(TabItems.FirstOrDefault(tab => tab.Header == tabItem.Header));
            }
                
        }
        public static void AddTab(TabItemFormat tabItemFormat)
        {
            TabItemFormat tabItem = TabItems.FirstOrDefault(tab => tab.Header == tabItemFormat.Header);
            if (tabItem == default)
            {
                TabItems.Add(tabItemFormat);
                changeSelectedTab?.Invoke(TabItems.IndexOf(tabItemFormat));
            }

            else
                changeSelectedTab?.Invoke(TabItems.IndexOf(tabItem));
        }
        public static void OpenDetailes(object param)
        {
            if (param != null)
            {
                Type t = param.GetType();
                int id = (int)t.GetProperty("Id").GetValue(param);
                TabItemFormat tab =
                t switch
                {
                    { } when t.Name.StartsWith("Drone") && !PLService.IsNotActiveDrone(id) => new TabItemFormat()
                    {
                        Header = "Drone " + id,
                        Content = new UpdateDroneVM(id)
                    },
                    { } when t.Name.StartsWith("Customer") && !PLService.IsNotActiveCustomer(id) => new TabItemFormat()
                    {
                        Header = "Customer " + id,
                        Content = new UpdateCustomerVM(id, true)
                    },
                    { } when t.Name.StartsWith("Station") && !PLService.IsNotActiveStation(id) => new TabItemFormat()
                    {
                        Header = "Station " + id,
                        Content = new UpdateStationVM(id)
                    },
                    { } when t.Name.StartsWith("Parcel") && !PLService.IsNotActiveParcel(id) => new TabItemFormat()
                    {
                        Header = "Parcel " + id,
                        Content = new UpdateParcelVM(id)
                    },
                    _ => null,
                };
                if (tab == null)
                    MessageBox.Show("Deleted");
                else
                    AddTab(tab);
            }
        }
    }
}
