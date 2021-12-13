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
            //how to render the suitable useer control in according to contect of link
            Button b = (Button)sender;
            TabItem tabItem = (TabItem)b.Parent;
            TabControl tab = (TabControl)tabItem.Parent;
            //.ItemsSource.Cast<TabItem>();
            List<TabItem> lst = (List<TabItem>)tab.ItemsSource;
            lst.Remove(tabItem);
        }
    }
}
