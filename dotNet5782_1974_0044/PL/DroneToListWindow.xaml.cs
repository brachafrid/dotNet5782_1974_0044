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

        private void close_tab(object sender, RoutedEventArgs e)
        {
            object tabItem = null;
            TabControl tabControl;
            object tmp=sender;
            while(tmp.GetType()!=typeof(TabControl))
            {
                tmp = ((FrameworkElement)tmp).Parent;
                if(tmp!=null && tmp.GetType()==typeof(TabItem))
                {
                    tabItem = tmp;
                }
            }
            tabControl = (TabControl)tmp;
            tabControl.Items.Remove(tabItem);
        }

        private void Add_tag_click(object sender, RoutedEventArgs e)
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
            tabItem.Content = new Drone((IBL.BO.DroneToList)((ListView)sender).Items.CurrentItem);
            tabItem.Header = "Drone";
            mainWindow.tab.Items.Add(tabItem);
        }
    }
    
}
