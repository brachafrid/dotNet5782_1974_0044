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
            ObservableCollection<IBL.BO.DroneToList> droneToLists = new();
            foreach (var item in ibal.GetDrones())
            {
                droneToLists.Add(item);
            }
            DataContext = droneToLists;
            //DataContext = ibal.GetDrones() as ObservableCollection<IBL.BO.DroneToList>;
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

        private void select_screen_out(object sender, SelectionChangedEventArgs e)
        {
            if (((e.OriginalSource as ComboBox).SelectedItem as ComboBoxItem).Content == "weight")
            {
                ChooseWeight.Visibility = Visibility.Visible;
                ChooseWeight.DataContext = Enum.GetValues(typeof(WeightCategories));
            }
            else
            {
                ChooseState.Visibility = Visibility.Visible;
                ChooseState.DataContext = Enum.GetValues(typeof(DroneState));
            }
        }

        private void select_screen_out_parameter(object sender, SelectionChangedEventArgs e)
        {
            if ((e.OriginalSource as ComboBox).Name == "ChooseWeight")
                DataContext = ibal.GetDronesScreenOut((WeightCategories weightCategories) => weightCategories.ToString() == ((e.OriginalSource as ComboBox).SelectedItem as ComboBoxItem).Content.ToString());
            else
                DataContext = ibal.GetDronesScreenOut((DroneState droneState) => droneState.ToString() == ((e.OriginalSource as ComboBox).SelectedItem as ComboBoxItem).Content.ToString());

        }
    }

}
