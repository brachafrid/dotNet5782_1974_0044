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
    }
}
