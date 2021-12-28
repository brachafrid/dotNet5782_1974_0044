using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Threading.Tasks;
using PL.PO;



namespace PL
{
    public class MainWindowVM : DependencyObject
    {
        public RelayCommand AdministratorLoginCommand { get; set; }
        public RelayCommand CustomerLoginCommand { get; set; }
        public RelayCommand ShowAdministratorLoginCommand { get; set; }
        public RelayCommand ShowCustomerLoginCommand { get; set; }
        public CustomerLogin customerLogin { get; set; }

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

        public Visibility VisibilityAdministrator
        {
            get { return (Visibility)GetValue(VisibilityAdministratorProperty); }
            set { SetValue(VisibilityAdministratorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilityAdministrator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityAdministratorProperty =
            DependencyProperty.Register("VisibilityAdministrator", typeof(Visibility), typeof(MainWindowVM), new PropertyMetadata(Visibility.Collapsed));



        public Visibility VisibilityLogin
        {
            get { return (Visibility)GetValue(VisibilityLoginProperty); }
            set { SetValue(VisibilityLoginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilityLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityLoginProperty =
            DependencyProperty.Register("VisibilityLogin", typeof(Visibility), typeof(MainWindowVM), new PropertyMetadata(Visibility.Visible));

        public Visibility VisibilityCustomer
        {
            get { return (Visibility)GetValue(VisibilityCustomerProperty); }
            set { SetValue(VisibilityCustomerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilityCustomer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityCustomerProperty =
            DependencyProperty.Register("VisibilityCustomer", typeof(Visibility), typeof(MainWindowVM), new PropertyMetadata(Visibility.Collapsed));



        public MainWindowVM()
        {
            ShowAdministratorLoginCommand = new RelayCommand(ShowAdministratorLogin, null);
            ShowCustomerLoginCommand = new RelayCommand(ShowCustomerLogin, null);
            AdministratorLoginCommand = new RelayCommand(AdministratorLogin, null);
            CustomerLoginCommand = new RelayCommand(CustomerLogin, null);
            customerLogin = new();
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
            if ((param as PasswordBox).Password == "1234")
            {
                VisibilityLogin = Visibility.Collapsed;
                VisibilityAdministrator = Visibility.Visible;
           }
            else
            {
                MessageBox.Show("Incorrect Password");
                (param as PasswordBox).Password = string.Empty;
            }

        }

        public void CustomerLogin(object param)
        {
            try
            {
                Customer customer = new CustomerHandler().GetCustomer((int)customerLogin.Id);
                VisibilityLogin = Visibility.Collapsed;
                VisibilityCustomer = Visibility.Visible;
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Incorrect Id");
                customerLogin.Id = null;
            }
        }

    }
}
