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
using IBL.BO;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneToList.xaml
    /// </summary>
    public partial class DroneToListWindow : UserControl
    {
        public IBL.IBL ibal;
        public ListCollectionView Drones { get; set; }
        public DroneToListWindow()
        {
            InitializeComponent();
            ibal = Singletone<IBL.BL>.Instance;
            ObservableCollection<IBL.BO.DroneToList> droneToLists = ibal.GetDrones() as ObservableCollection<IBL.BO.DroneToList>;
            Drones = new ListCollectionView(droneToLists);
            Drones.Filter += FilterDrones;
            Drones.IsLiveFiltering = true;
            DataContext = Drones;
            ChooseWeight.DataContext = Enum.GetValues(typeof(WeightCategories));
            ChooseState.DataContext = Enum.GetValues(typeof(DroneState));
        }

        private bool FilterDrones(object obj)
        {
            if (obj is IBL.BO.DroneToList droneList)
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

        private void Close_tab_click(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(MainWindow))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            (tmp as MainWindow).DroneToListTab.Visibility = Visibility.Collapsed;
            (tmp as MainWindow).contentDroneToListTab.Visibility = Visibility.Collapsed;
        }

        private void Add_tab_click(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(MainWindow))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone();
            tabItem.Header = (sender as Button).Content.ToString();
            (tmp as MainWindow).tab.Items.Add(tabItem);
        }

        private void double_click(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext is IBL.BO.DroneToList)
            {
                object tmp = sender;
                while (tmp.GetType() != typeof(MainWindow))
                {
                    tmp = ((FrameworkElement)tmp).Parent;
                }
                MainWindow mainWindow = (MainWindow)tmp;
                TabItem tabItem = new TabItem();
                tabItem.Content = new Drone((e.OriginalSource as FrameworkElement).DataContext as IBL.BO.DroneToList);
                tabItem.Header = "Drone";
                mainWindow.tab.Items.Add(tabItem);
            }
        }

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

        private void select_screen_out_parameter(object sender, SelectionChangedEventArgs e)
        {
                Drones.Filter = FilterDrones;
        }

        private void Cancel_screen_out(object sender, RoutedEventArgs e)
        {
            ChooseWeight.Visibility = ChooseState.Visibility = Visibility.Collapsed;
            selectCategory.SelectedItem = ChooseWeight.SelectedItem = ChooseState.SelectedItem = null;
        }

        public ObservableCollection<IBL.BO.DroneToList> ConvertDroneToList(IEnumerable<DroneToList> IbalDroneToLists)
        {
            ObservableCollection<IBL.BO.DroneToList> droneToLists = new ObservableCollection<DroneToList>(IbalDroneToLists);
            return droneToLists;
        }

    }

}
