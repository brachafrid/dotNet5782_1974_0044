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
    /// <summary>
    /// Interaction logic for DroneToList.xaml
    /// </summary>
    public partial class DroneToListWindow : UserControl
    {
        public BLApi.IBL ibal;
        public ListCollectionView Drones { get; set; }
        private Action updateList;
        private MainWindow MainWindow;
        /// <summary>
        /// Constructor DroneToList Window.
        /// Initializes necessary things
        /// </summary>
        /// <param name="mainWindow">Main window</param>
        public DroneToListWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            ibal = BLFactory.GetBL();
            Drones = new ListCollectionView(ibal.GetDrones() as IList);
            Drones.Filter += FilterDrones;
            Drones.IsLiveFiltering = true;
            DataContext = Drones;
            ChooseWeight.DataContext = Enum.GetValues(typeof(WeightCategories));
            ChooseState.DataContext = Enum.GetValues(typeof(DroneState));
            updateList = UpdateNewList;
            MainWindow = mainWindow;
        }

        /// <summary>
        /// Update the list of the drones
        /// </summary>
        private void UpdateNewList()
        {

            Drones = new ListCollectionView(ibal.GetDrones() as IList);
            Drones.Filter += FilterDrones;
            Drones.IsLiveFiltering = true;
            DataContext = Drones;
        }

        /// <summary>
        /// Filter the list of drones. 
        /// return if the drone with weight or/and status 
        /// according to the user's choice
        /// </summary>
        /// <param name="obj">The drone</param>
        /// <returns>return if drone with the selected weight or/and the selected status</returns>
        private bool FilterDrones(object obj)
        {
            if (obj is BO.DroneToList droneList)
            {
                if (ChooseWeight.SelectedItem != null && ChooseState.SelectedItem != null)
                    return droneList.Weight == (ChooseWeight.SelectedItem as WeightCategories?) && droneList.DroneState == (ChooseState.SelectedItem as DroneState?);
                else
                {
                    if (ChooseWeight.SelectedItem != null)
                        return droneList.Weight == (ChooseWeight.SelectedItem as WeightCategories?);
                    if (ChooseState.SelectedItem != null)
                        return droneList.DroneState == (ChooseState.SelectedItem as DroneState?);
                }

            }
            return true;
        }

        /// <summary>
        /// Close the tab of the drone
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Close_tab_click(object sender, RoutedEventArgs e)
        {
            MainWindow.DroneToListTab.Visibility = Visibility.Collapsed;
            (MainWindow.DroneToListTab.Content as FrameworkElement).Visibility = Visibility.Collapsed;
            
        }

        /// <summary>
        /// Add drone tab in insert mode
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Add_tab_click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new DroneWindow(updateList,MainWindow);
            tabItem.Header = (sender as Button).Content.ToString();
            MainWindow.tab.Items.Add(tabItem);
            MainWindow.tab.SelectedItem = tabItem;
        }

        /// <summary>
        /// Adds the drone tab in update mode
        /// when double-clicking a skimmer in the skimmer list
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void double_click(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext is BO.DroneToList)
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new DroneWindow((e.OriginalSource as FrameworkElement).DataContext as BO.DroneToList, updateList, MainWindow);
                tabItem.Header = "Drone";
                MainWindow.tab.Items.Add(tabItem);
                MainWindow.tab.SelectedItem = tabItem;
            }
        }

        /// <summary>
        /// Adds selection boxes for filtering the drones by weight and by condition
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void select_screen_out(object sender, SelectionChangedEventArgs e)
        {
            if ((e.OriginalSource as ComboBox).SelectedItem != null)
            {

                if (((e.OriginalSource as ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "weight")
                    ChooseWeight.Visibility = Visibility.Visible;
                else if ((e.OriginalSource as ComboBox).SelectedItem != null)
                    ChooseState.Visibility = Visibility.Visible;
            }

        }

        /// <summary>
        /// Filter the list of drones according to the user's choice. 
        /// per selected status or/and selected weight
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void select_screen_out_parameter(object sender, SelectionChangedEventArgs e)
        {
              Drones.Filter = FilterDrones;
        }

        /// <summary>
        /// Closes the selection boxes for filtering the drones
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Cancel_screen_out(object sender, RoutedEventArgs e)
        {
            ChooseWeight.Visibility = ChooseState.Visibility = Visibility.Collapsed;
            selectCategory.SelectedItem = ChooseWeight.SelectedItem = ChooseState.SelectedItem = null;
        }

        public ObservableCollection<BO.DroneToList> ConvertDroneToList(IEnumerable<DroneToList> IbalDroneToLists)
        {
            ObservableCollection<BO.DroneToList> droneToLists = new ObservableCollection<DroneToList>(IbalDroneToLists);
            return droneToLists;
        }
    }
}
