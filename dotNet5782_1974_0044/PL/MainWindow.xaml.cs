using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BL.BLApi.IBL ibal;
        private List<string> option = new() { "DroneToList"};

        public MainWindow()
        { 
          InitializeComponent();
            ibal = Singletone<BL.BL>.Instance;
            DataContext = option;
            DroneToListTab.DataContext = new DroneToListWindow(this);
        }

        public void Add_tag_click(object sender, RoutedEventArgs e)
        {
            DroneToListTab.Visibility = Visibility.Visible;
            (DroneToListTab.Content as FrameworkElement).Visibility = Visibility.Visible;
        }
        public void Close_tab(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(TabItem))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            tab.Items.Remove(tmp as TabItem);
        }
    }
}
