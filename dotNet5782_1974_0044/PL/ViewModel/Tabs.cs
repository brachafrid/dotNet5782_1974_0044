using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL
{
    public static class Tabs
    {
        /// <summary>
        /// ObservableCollection of the tabs
        /// </summary>
        public static ObservableCollection<TabItemFormat> TabItems { get; set; } = new();
        /// <summary>
        /// Command of closing tab
        /// </summary>
        public static RelayCommand CloseCommandTab { get; set; }
        /// <summary>
        /// Action- changes to the selected tab
        /// </summary>
        public static Action<int> changeSelectedTab;

        /// <summary>
        /// constructor
        /// </summary>
        static Tabs()
        {
            CloseCommandTab = new(CloseTab, null);
        }

        /// <summary>
        /// Close tab
        /// </summary>
        /// <param name="param"></param>
        public static void CloseTab(object param)
        {
            if (param is TabItemFormat tabItem)
            {
                tabItem.Dispose();
                TabItems.Remove(TabItems.FirstOrDefault(tab => tab.Header == tabItem.Header));
            }
        }

        /// <summary>
        /// Add tab
        /// </summary>
        /// <param name="tabItemFormat"></param>
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

        /// <summary>
        /// Open details
        /// </summary>
        /// <param name="param"></param>
        public async static void OpenDetailes(object param)
        {
            try
            {
                if (param != null)
                {
                    Type t = param.GetType();
                    int id = (int)t.GetProperty("Id").GetValue(param);
                    TabItemFormat tab =
                    t switch
                    {
                        { } when t.Name.StartsWith("Drone") && !await PLService.IsNotActiveDrone(id) => new TabItemFormat()
                        {
                            Header = "Drone " + id,
                            Content = new UpdateDroneVM(id)
                        },
                        { } when t.Name.StartsWith("Customer") && !await PLService.IsNotActiveCustomer(id) => new TabItemFormat()
                        {
                            Header = "Customer " + id,
                            Content = new UpdateCustomerVM(id, true)
                        },
                        { } when t.Name.StartsWith("Station") && !await PLService.IsNotActiveStation(id) => new TabItemFormat()
                        {
                            Header = "Station " + id,
                            Content = new UpdateStationVM(id)
                        },
                        { } when t.Name.StartsWith("Parcel") && !await PLService.IsNotActiveParcel(id) => new TabItemFormat()
                        {
                            Header = "Parcel " + id,
                            Content = new UpdateParcelVM(id)
                        },
                        _ => null,
                    };
                    if (tab == null)
                        MessageBox.Show("deleted", "Adding Tab", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                        AddTab(tab);
                }
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message ,"Adding Tab",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Adding Tab", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Log out from the current screen
        /// </summary>
        /// <param name="param"></param>
        static internal void LogOut(object param)
        {
            TabItems.ToList().ForEach(tab => CloseTab(tab));
            LoginScreen.MyScreen = Screen.LOGIN;
            LoginScreen.Id = null;
        }

        /// <summary>
        /// Refresh the screen
        /// </summary>
        /// <param name="param"></param>
        static internal void Refresh(object param)
        {
            RefreshEvents.NotifyCustomerChanged();
            RefreshEvents.NotifyDroneChanged();
            RefreshEvents.NotifyParcelChanged();
            RefreshEvents.NotifyStationChanged();
        }
    }
}
