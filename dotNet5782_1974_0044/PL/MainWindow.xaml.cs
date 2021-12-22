using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLApi;
using Utilities;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BLApi.IBL ibal;
        private List<string> option = new() { "Drones","Customers","Stations","Parcels"};
        /// Initializes necessary things
        /// </summary>
        public MainWindow()
        { 
            InitializeComponent();
            ibal = BLFactory.GetBL();
            DataContext = option;
            DroneToListTab.DataContext = new DroneToListWindow(this);
        }

        /// <summary>
        /// Displays the list of drones
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        public void Add_tag_click(object sender, RoutedEventArgs e)
        {
            TabItem currentTab = (sender as Button).Content switch
            {
                "Drones"=>DroneToListTab,
                "Parcels"=>ParcelsToListTab,
                "Stations"=>StationToListTab,
                "Customers"=>CustomerToListTab
            };
            currentTab.Visibility = Visibility.Visible;
            tab.SelectedItem = currentTab;
            if (currentTab == DroneToListTab)
                (DroneToListTab.Content as FrameworkElement).Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Close tab 
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        public void Close_tab(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(TabItem))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            tab.Items.Remove(tmp as TabItem);
        }

        private void StationViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            StationToListView stationToListView = new StationToListView();
            stationToListView.LoadStations();
            //StationToListControl.DataContext = stationToListView;
        }

        private void CustomerViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerToListViewModel customerToListViewModel = new CustomerToListViewModel(this);
            customerToListViewModel.LoadCustomers();
            CustomerToListControl.DataContext = customerToListViewModel;
        }
    }
}
