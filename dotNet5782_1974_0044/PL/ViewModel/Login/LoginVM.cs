using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PL.PO;

namespace PL
{
    public class LoginVM : DependencyObject
    {
        public LoginVM()
        {
            ShowAdministratorLoginCommand = new RelayCommand(ShowAdministratorLogin, null);
            ShowCustomerLoginCommand = new RelayCommand(ShowCustomerLogin, null);
            AdministratorLoginCommand = new RelayCommand(AdministratorLogin, null);
            CustomerLoginCommand = new RelayCommand(CustomerLogin, null);
            ShowCustomerSigninCommand = new(ShowCustomerSignin, null);
        }
        public RelayCommand AdministratorLoginCommand { get; set; }
        public RelayCommand CustomerLoginCommand { get; set; }
        public RelayCommand CustomerSigninCommand { get; set; }
        public RelayCommand ShowAdministratorLoginCommand { get; set; }
        public RelayCommand ShowCustomerLoginCommand { get; set; }
        public RelayCommand ShowCustomerSigninCommand { get; set; }
        public CustomerLogin customerLogin { get; set; } = new();
        public Visble VisibilityAdministratorLogin { get; set; } = new();      
        public Visble VisibilityCustomerLogin { get; set; } = new();  
        public Visble VisibilityCustomerSignIn { get; set; } = new();  
        public void ShowAdministratorLogin(object param)
        {
            VisibilityAdministratorLogin.visibility = Visibility.Visible;
            VisibilityCustomerLogin.visibility = Visibility.Collapsed;
            VisibilityCustomerSignIn.visibility = Visibility.Collapsed;
        }
        public void ShowCustomerSignin(object param)
        {
            VisibilityAdministratorLogin.visibility = Visibility.Collapsed;
            VisibilityCustomerLogin.visibility = Visibility.Collapsed;
            VisibilityCustomerSignIn.visibility = Visibility.Visible;
        }
        public void ShowCustomerLogin(object param)
        {
            VisibilityCustomerLogin.visibility = Visibility.Visible;
            VisibilityAdministratorLogin.visibility = Visibility.Collapsed;
            VisibilityCustomerSignIn.visibility = Visibility.Collapsed;
        }
        public void AdministratorLogin(object param)
        {
            if ((param as PasswordBox).Password == PLService.GetAdministorPasssword())
            {
                LoginScreen.MyScreen = "AdministratorWindow";
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
                Customer customer = PLService.GetCustomer((int)customerLogin.Id);
                LoginScreen.Id = customer.Id;
                LoginScreen.MyScreen = "CustomerWindow";
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Incorrect Id");
                customerLogin.Id = null;
            }
        }
        public void SignIn(object param)
        {

        }
    }
}

