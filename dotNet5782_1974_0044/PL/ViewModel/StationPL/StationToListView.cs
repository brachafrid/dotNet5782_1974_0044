using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;
using BO;
using BLApi;
using System.Collections.ObjectModel;
using System.Collections;

namespace PL
{
    public class StationToListView
    {
        public BLApi.IBL ibal;
        public ObservableCollection<StationToList> Stations { get; set; }
        private MainWindow MainWindow;
        /// <summary>
        /// Constructor DroneToList Window.
        /// Initializes necessary things
        /// </summary>
        /// <param name="mainWindow">Main window</param>
        public StationToListView(MainWindow mainWindow)
        {
            ibal = BLFactory.GetBL();
            MainWindow = mainWindow;
        }

        public void LoadStations()
        {
            Stations = new ObservableCollection<StationToList>(ibal.GetStations());
        }

        ///// <summary>
        ///// Close the tab of the drone
        ///// </summary>
        ///// <param name="sender">Event operator</param>
        ///// <param name="e">The arguments of the event</param>
        //private void Close_tab_click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow.DroneToListTab.Visibility = Visibility.Collapsed;
        //    (MainWindow.DroneToListTab.Content as FrameworkElement).Visibility = Visibility.Collapsed;

        //}

        ///// <summary>
        ///// Add drone tab in insert mode
        ///// </summary>
        ///// <param name="sender">Event operator</param>
        ///// <param name="e">The arguments of the event</param>
        //private void Add_tab_click(object sender, RoutedEventArgs e)
        //{
        //    TabItem tabItem = new TabItem();
        //    tabItem.Content = new StationWindow();
        //    tabItem.Header = (sender as Button).Content.ToString();
        //    MainWindow.tab.Items.Add(tabItem);
        //    MainWindow.tab.SelectedItem = tabItem;
        //}

        ///// <summary>
        ///// Adds the drone tab in update mode
        ///// when double-clicking a skimmer in the skimmer list
        ///// </summary>
        ///// <param name="sender">Event operator</param>
        ///// <param name="e">The arguments of the event</param>
        //private void double_click(object sender, MouseButtonEventArgs e)
        //{
        //    if (((FrameworkElement)e.OriginalSource).DataContext is BO.DroneToList)
        //    {
        //        TabItem tabItem = new TabItem();
        //        tabItem.Content = new DroneWindow((e.OriginalSource as FrameworkElement).DataContext as BO.DroneToList, updateList, MainWindow);
        //        tabItem.Header = "Drone";
        //        MainWindow.tab.Items.Add(tabItem);
        //        MainWindow.tab.SelectedItem = tabItem;
        //    }
        //}

    }
}


