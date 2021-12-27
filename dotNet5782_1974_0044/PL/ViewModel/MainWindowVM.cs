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
    public class MainWindowVM : DependencyObject
    {
        public RelayCommand AdministratorLoginCommand { get; set; }
        public RelayCommand CustomerLoginCommand { get; set; }
        public RelayCommand ShowAdministratorLoginCommand { get; set; }
        public RelayCommand ShowCustomerLoginCommand { get; set; }

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
        // Using a DependencyProperty as the backing store for VisibilityCustomerLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityCustomerLoginProperty =
            DependencyProperty.Register("VisibilityCustomerLogin", typeof(Visibility), typeof(MainWindowVM), new PropertyMetadata(Visibility.Collapsed));

        public MainWindowVM()
        {
            ShowAdministratorLoginCommand = new RelayCommand(ShowAdministratorLogin, null);
            ShowCustomerLoginCommand = new RelayCommand(ShowCustomerLogin, null);
            AdministratorLoginCommand = new RelayCommand(AdministratorLogin, null);
            //CustomerLoginCommand = new RelayCommand(AdministratorLogin, null);
            //VisibilityAdministratorLogin = Visibility.Collapsed;
            //VisibilityCustomerLogin = Visibility.Collapsed;
        }
        public void ShowAdministratorLogin(object param)
        {
            VisibilityAdministratorLogin = Visibility.Visible;
            VisibilityCustomerLogin = Visibility.Collapsed;
        }
        public void ShowCustomerLogin(object param)
        {
            VisibilityCustomerLogin = Visibility.Visible;
            VisibilityAdministratorLogin = Visibility.Collapsed;
        }

        public void AdministratorLogin(object param)
        {
            if (param is PasswordBox && (param as PasswordBox).Password == "1234")
            {
                MessageBox.Show("wwelcome!!!!!!!!!!!!!");
            }
            else
                MessageBox.Show("Administrator password is not correct");
        }

        public void CustomerLogin(object param)
        {
            if (param is PasswordBox && (param as PasswordBox).Password == "1234")
            {
                MessageBox.Show("wwelcome!!!!!!!!!!!!!");
            }
            else
                MessageBox.Show("Administrator password is not correct");
        }

    }
}
