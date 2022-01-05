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
        public static Action<int> changeSelectedTab;
        public static void CloseTab(string Text)
        {
            TabItems.Remove(TabItems.FirstOrDefault(tab => tab.Header == Text));
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
        public static void OpenCustomerDetails(object param)
        {
            if (param != null && param is int Id)
                AddTab(new()
                {
                    Header = "customer " + Id,
                    Content =new UpdateCustomerVM(Id)
                });
        }
        public static void OpenParcelDetails(object param)
        {
            if (param != null && param is int Id)
                AddTab(new()
                {

                    Header = "parcel " + Id,
                    Content = new UpdateParcelVM(Id)
                });
        }
        public static void OpenDroneDetails(object param)
        {
            if (param != null && param is PL.PO.DroneInCharging droneCharge)
               AddTab(new()
                {
                    Header = "drone " + droneCharge.Id,
                    Content = new UpdateDroneVM(droneCharge.Id)
                });
        }
        public static void  OpenStationDetails(object param)
        {
            if (param != null)
                AddTab(new TabItemFormat()
                {

                    Header = "station " + (param as StationToList).Id,
                    Content = new UpdateStationVM((param as StationToList).Id)

                });
        }
    }
}
