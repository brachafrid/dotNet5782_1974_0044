using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Threading.Tasks;



namespace PL
{
    public class MainWindowVM:DependencyObject
    {
        public RelayCommand CommandAdministrator { get; set; }
        public RelayCommand ShowCommandAdministrator { get; set; }
        public RelayCommand ShowCommandCustomer { get; set; }

        //public Visibility VisibilityAdministratorLogin { get; set; } = Visibility.Collapsed;
        //public Visibility VisibilityCustomerLogin { get; set; } = Visibility.Collapsed;

        public Visibility VisibilityAdministratorLogin
        {
            get { return (Visibility)GetValue(VisibilityAdministratorLoginProperty); }
            set { SetValue(VisibilityAdministratorLoginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilityAdministratorLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityAdministratorLoginProperty =
            DependencyProperty.Register("VisibilityAdministratorLogin", typeof(Visibility), typeof(MainWindowVM), new PropertyMetadata(Visibility.Collapsed));

        public Visibility VisibilityCustomerLogin
        {
            get { return (Visibility)GetValue(VisibilityCustomerLoginProperty); }
            set { SetValue(VisibilityCustomerLoginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilityAdministratorLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityCustomerLoginProperty =
            DependencyProperty.Register("VisibilityAdministratorLogin", typeof(Visibility), typeof(MainWindowVM), new PropertyMetadata(Visibility.Collapsed));

        public string Password { get; set; }
        public MainWindowVM()
        {
            ShowCommandAdministrator = new RelayCommand(ShowAdministratorLogin, null);
            ShowCommandCustomer = new RelayCommand(ShowCustomerLogin, null);
            VisibilityAdministratorLogin = Visibility.Collapsed;
            VisibilityCustomerLogin = Visibility.Collapsed;
        }
        public void ShowAdministratorLogin(object param)
        {
            VisibilityAdministratorLogin = Visibility.Visible;
        }
        public void ShowCustomerLogin(object param)
        {
            VisibilityCustomerLogin = Visibility.Visible;
        }
    }
}
