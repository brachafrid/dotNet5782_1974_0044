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
        public static void OpenDetailes(object param)
        {
            if (param != null)
            {
                Type t = param.GetType();
                int id = (int)t.GetProperty("Id").GetValue(param);
                AddTab(
                    t switch
                    {
                        { } when t.Name.Contains("Drone") => new TabItemFormat()
                        {
                            Header = "Drone " + id,
                            Content = new UpdateDroneVM(id)
                        },
                        { } when t.Name.Contains("Customer") => new TabItemFormat()
                        {
                            Header = "Customer " + id,
                            Content = new UpdateDroneVM(id)
                        },
                        { } when t.Name.Contains("Station") => new TabItemFormat()
                        {
                            Header = "Station " + id,
                            Content = new UpdateDroneVM(id)
                        },
                        { } when t.Name.Contains("Parcel") => new TabItemFormat()
                        {
                            Header = "Parcel " + id,
                            Content = new UpdateDroneVM(id)
                        },
                    }
                    ) ;
            }
                

        }
    }
}
