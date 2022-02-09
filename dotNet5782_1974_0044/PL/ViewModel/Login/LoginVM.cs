using PL.PO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    public class LoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
        public LoginVM()
        {
            Add = new(true);
            ShowAdministratorLoginCommand = new RelayCommand(ShowAdministratorLogin, null);
            ShowCustomerLoginCommand = new RelayCommand(ShowCustomerLogin, null);
            AdministratorLoginCommand = new RelayCommand(AdministratorLogin, null);
            CustomerLoginCommand = new RelayCommand(CustomerLogin, null);
            ShowCustomerSigninCommand = new(ShowCustomerSignin, null);
        }
        public CustomerAddVM Add { get; set; }
        public RelayCommand AdministratorLoginCommand { get; set; }
        public RelayCommand CustomerLoginCommand { get; set; }
        public RelayCommand ShowAdministratorLoginCommand { get; set; }
        public RelayCommand ShowCustomerLoginCommand { get; set; }
        public RelayCommand ShowCustomerSigninCommand { get; set; }
        public CustomerLogin customerLogin { get; set; } = new();
        private Visibility visibilityAdministratorLogin=Visibility.Collapsed;

        public Visibility VisibilityAdministratorLogin
        {
            get { return visibilityAdministratorLogin; }
            set { visibilityAdministratorLogin = value;
                onPropertyChanged("VisibilityAdministratorLogin");
            }
        }

        private Visibility visibilityCustomerLogin = Visibility.Collapsed;

        public Visibility VisibilityCustomerLogin
        {
            get { return visibilityCustomerLogin; }
            set
            {
                visibilityCustomerLogin = value;
                onPropertyChanged("VisibilityCustomerLogin");
            }
        }


        private Visibility visibilityCustomerSignIn = Visibility.Collapsed;

        public Visibility VisibilityCustomerSignIn
        {
            get { return visibilityCustomerSignIn; }
            set
            {
                visibilityCustomerSignIn = value;
                onPropertyChanged("VisibilityCustomerSignIn");
            }
        }

        public void ShowAdministratorLogin(object param)
        {
            VisibilityAdministratorLogin = Visibility.Visible;
            VisibilityCustomerLogin = Visibility.Collapsed;
            VisibilityCustomerSignIn = Visibility.Collapsed;
        }
        public void ShowCustomerSignin(object param)
        {
            VisibilityAdministratorLogin = Visibility.Collapsed;
            VisibilityCustomerLogin = Visibility.Collapsed;
            VisibilityCustomerSignIn = Visibility.Visible;
        }
        public void ShowCustomerLogin(object param)
        {
            VisibilityCustomerLogin = Visibility.Visible;
            VisibilityAdministratorLogin = Visibility.Collapsed;
            VisibilityCustomerSignIn = Visibility.Collapsed;
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
                if (PLService.IsNotActiveCustomer(customer.Id))
                    MessageBox.Show("Deleted customer");
                else
                {
                    LoginScreen.Id = customer.Id;
                    LoginScreen.MyScreen = "CustomerWindow";
                }
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Incorrect Id");
                customerLogin.Id = null;
            }
        }
    }
}

