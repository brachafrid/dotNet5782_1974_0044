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
    public class CustomerToListViewModel
    {
        public ObservableCollection<CustomerToList> Customers { get; set; }
        public BLApi.IBL ibal;
        private MainWindow MainWindow;

        /// <summary>
        /// Constructor CustomerToList Window.
        /// Initializes necessary things
        /// </summary>
        /// <param name="mainWindow">Main window</param>
        public CustomerToListViewModel(MainWindow mainWindow)
        {
            ibal = BLFactory.GetBL();

            MainWindow = mainWindow;
        }
        public CustomerToListViewModel(CustomerToList customerToList, MainWindow mainWindow)
        {
            ibal = BLFactory.GetBL();

            MainWindow = mainWindow;
        }
        public void LoadCustomers()
        {
            Customers = new ObservableCollection<CustomerToList>(ibal.GetCustomers());
        }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler((obj) => MyAction(obj), true));
            }
        }

        public void MyAction(object obj)
        {
            (obj as MainWindow).CustomerToListTab.Visibility = Visibility.Collapsed;
            MessageBox.Show("fffffffffffff");
        }

    }
    //public class CommandHandler : ICommand
    //{
    //    private Action<object> _action;
    //    private bool _canExecute;
    //    public CommandHandler(Action<object> action, bool canExecute)
    //    {
    //        _action = action;
    //        _canExecute = canExecute;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return _canExecute;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //        _action(parameter);
    //    }
    //}


    ///// <summary>
    ///// Close the tab of the customer
    ///// </summary>
    ///// <param name="sender">Event operator</param>
    ///// <param name="e">The arguments of the event</param>
    //private void Close_tab_click(object sender, RoutedEventArgs e)
    //{
    //    MainWindow.CustomerToListTab.Visibility = Visibility.Collapsed;
    //    (MainWindow.CustomerToListTab.Content as FrameworkElement).Visibility = Visibility.Collapsed;

    //}

    ///// <summary>
    ///// Add drone tab in insert mode
    ///// </summary>
    ///// <param name="sender">Event operator</param>
    ///// <param name="e">The arguments of the event</param>
    //private void Add_tab_click(object sender, RoutedEventArgs e)
    //{
    //    TabItem tabItem = new TabItem();
    //    tabItem.Content = new CustomerToListViewModel(MainWindow);
    //    tabItem.Header = (sender as Button).Content.ToString();
    //    MainWindow.tab.Items.Add(tabItem);
    //    MainWindow.tab.SelectedItem = tabItem;
    //}

    ///// <summary>
    ///// Adds the drone tab in update mode
    ///// when double-clicking a skimmer in the skimmer list
    ///// </summary>
    ///// <param name="sender">Event operator</param>
    ///// <param name="e">The arguments of the event</param>
    //private void double_click(object sender, MouseButtonEventArgs e)
    //{
    //    if (((FrameworkElement)e.OriginalSource).DataContext is BO.CustomerToList)
    //    {
    //        TabItem tabItem = new TabItem();
    //        tabItem.Content = new CustomerToListViewModel((e.OriginalSource as FrameworkElement).DataContext as BO.CustomerToList, MainWindow);
    //        tabItem.Header = "Customer";
    //        MainWindow.tab.Items.Add(tabItem);
    //        MainWindow.tab.SelectedItem = tabItem;
    //    }
    //}
}

