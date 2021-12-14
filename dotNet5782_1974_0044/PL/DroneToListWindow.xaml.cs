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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneToList.xaml
    /// </summary>
    public partial class DroneToList : UserControl
    {
        public IBL.IBL ibal;
        public DroneToList()
        {
            InitializeComponent();
            ibal = Singletone<IBL.BL>.Instance;
            DataContext = ibal.GetDrones();
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

        }
    }
    
}
