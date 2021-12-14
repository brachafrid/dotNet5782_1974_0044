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
        public DroneToListWindow()
        {
            InitializeComponent();
            ibal = Singletone<IBL.BL>.Instance;
            DataContext = ConvertDroneToList(ibal.GetDrones());
        }

        private void Close_tab_click(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(MainWindow))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            MainWindow mainWindow = (MainWindow)tmp;
            mainWindow.Close_tab(sender, e);
        }

        private void Add_tab_click(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(MainWindow))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            MainWindow mainWindow = (MainWindow)tmp;
            mainWindow.Add_tag_click(sender, e);
        }

        private void double_click(object sender, MouseButtonEventArgs e)
        {
            if(((FrameworkElement)e.OriginalSource).DataContext is IBL.BO.DroneToList)
            {
                object tmp = sender;
                while (tmp.GetType() != typeof(MainWindow))
                {
                    tmp = ((FrameworkElement)tmp).Parent;
                }
                MainWindow mainWindow = (MainWindow)tmp;
                TabItem tabItem = new TabItem();
                tabItem.Content = new Drone((IBL.BO.DroneToList)((FrameworkElement)e.OriginalSource).DataContext);
                tabItem.Header = "Drone";
                mainWindow.tab.Items.Add(tabItem);
            }
        }

        private void select_screen_out(object sender, SelectionChangedEventArgs e)
        {
            if ((e.OriginalSource as ComboBox).SelectedItem != null &&((e.OriginalSource as ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "weight")
            {
                ChooseWeight.Visibility = Visibility.Visible;
                ChooseWeight.DataContext = Enum.GetValues(typeof(WeightCategories));
            }
            else if((e.OriginalSource as ComboBox).SelectedItem != null)
            {
                ChooseState.Visibility = Visibility.Visible;
                ChooseState.DataContext = Enum.GetValues(typeof(DroneState));
            }
        }

        private void select_screen_out_parameter(object sender, SelectionChangedEventArgs e)
        {
            string cont = (e.OriginalSource as ComboBox).SelectedItem.ToString();
            if ((e.OriginalSource as ComboBox).Name == "ChooseWeight")
                DataContext = ConvertDroneToList( ibal.GetDronesScreenOut((WeightCategories weightCategories) => weightCategories.ToString() == cont));
            else
                DataContext = ConvertDroneToList(ibal.GetDronesScreenOut((DroneState droneState) => droneState.ToString() == cont));

        }

        private void Cancel_screen_out(object sender, RoutedEventArgs e)
        {
            ChooseWeight.Visibility = Visibility.Collapsed;
            ChooseState.Visibility = Visibility.Collapsed;
            DataContext = ConvertDroneToList(ibal.GetDrones());
            selectCategory.SelectedItem = null;
        }

        private ObservableCollection<IBL.BO.DroneToList> ConvertDroneToList(IEnumerable<DroneToList> IbalDroneToLists)
        {
            ObservableCollection<IBL.BO.DroneToList> droneToLists = new();
            foreach (var item in IbalDroneToLists)
            {
                droneToLists.Add(item);
            }
            return droneToLists;
        }

    }

}
