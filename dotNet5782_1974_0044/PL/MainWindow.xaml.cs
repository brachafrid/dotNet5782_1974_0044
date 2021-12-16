﻿using System;
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
using IBL;
using Utilities;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL.IBL ibal;
        private List<string> option = new() { "DroneToList"};
        public MainWindow()
        {
            InitializeComponent();
            ibal = Singletone<IBL.BL>.Instance;
            DataContext = option;
        }

        public void Add_tag_click(object sender, RoutedEventArgs e)
        {
            DroneToListTab.Visibility = Visibility.Visible;
            contentDroneToListTab.Visibility = Visibility.Visible;
        }
        public void Close_tab(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(TabItem))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            TabItem tabItem = (TabItem)tmp;
            tab.Items.Remove(tabItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
