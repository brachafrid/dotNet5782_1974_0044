using PL.PO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    public class LoginVM : NotifyPropertyChangedBase
    {

        /// <summary>
        /// constructor
        /// </summary>
        public LoginVM()
        {
            Add = new(true);
            ShowAdministratorLoginCommand = new RelayCommand(ShowAdministratorLogin, null);
            ShowCustomerLoginCommand = new RelayCommand(ShowCustomerLogin, null);
            AdministratorLoginCommand = new RelayCommand(AdministratorLogin, null);
            CustomerLoginCommand = new RelayCommand(CustomerLogin, null);
            ShowCustomerSigninCommand = new(ShowCustomerSignin, null);
        }
        /// <summary>
        /// Customer Add view model
        /// </summary>
        public CustomerAddVM Add { get; set; }
        /// <summary>
        /// Command of administrator login
        /// </summary>
        public RelayCommand AdministratorLoginCommand { get; set; }
        /// <summary>
        /// Command of customer login
        /// </summary>
        public RelayCommand CustomerLoginCommand { get; set; }
        /// <summary>
        /// Command of showing administrator login
        /// </summary>
        public RelayCommand ShowAdministratorLoginCommand { get; set; }
        /// <summary>
        /// Command of showing customer login
        /// </summary>
        public RelayCommand ShowCustomerLoginCommand { get; set; }
        /// <summary>
        /// Command of showing customer signin
        /// </summary>
        public RelayCommand ShowCustomerSigninCommand { get; set; }
        /// <summary>
        /// Command of customer login
        /// </summary>
        public CustomerLogin customerLogin { get; set; } = new();
        private Visibility visibilityAdministratorLogin = Visibility.Collapsed;

        /// <summary>
        /// Visibility of administrator login
        /// </summary>
        public Visibility VisibilityAdministratorLogin
        {
            get => visibilityAdministratorLogin; 
            set => Set(ref visibilityAdministratorLogin, value);
        }

        private Visibility visibilityCustomerLogin = Visibility.Collapsed;

        /// <summary>
        /// Visibility of customer login
        /// </summary>
        public Visibility VisibilityCustomerLogin
        {
            get =>  visibilityCustomerLogin; 
            set => Set(ref visibilityCustomerLogin, value);
        }
        private Visibility visibilityCustomerSignIn = Visibility.Collapsed;

        /// <summary>
        /// Visibility of customer sign in
        /// </summary>
        public Visibility VisibilityCustomerSignIn
        {
            get => visibilityCustomerSignIn; 
            set => Set(ref visibilityCustomerSignIn, value);
        }

        /// <summary>
        /// Show administrator login
        /// </summary>
        /// <param name="param"></param>
        public void ShowAdministratorLogin(object param)
        {
            VisibilityAdministratorLogin = Visibility.Visible;
            VisibilityCustomerLogin = Visibility.Collapsed;
            VisibilityCustomerSignIn = Visibility.Collapsed;
        }

        /// <summary>
        /// Show customer sign in
        /// </summary>
        /// <param name="param"></param>
        public void ShowCustomerSignin(object param)
        {
            VisibilityAdministratorLogin = Visibility.Collapsed;
            VisibilityCustomerLogin = Visibility.Collapsed;
            VisibilityCustomerSignIn = Visibility.Visible;
        }

        /// <summary>
        /// Show customer login
        /// </summary>
        /// <param name="param"></param>
        public void ShowCustomerLogin(object param)
        {
            VisibilityCustomerLogin = Visibility.Visible;
            VisibilityAdministratorLogin = Visibility.Collapsed;
            VisibilityCustomerSignIn = Visibility.Collapsed;
        }

        /// <summary>
        /// Administrator login
        /// </summary>
        /// <param name="param"></param>
        public async void AdministratorLogin(object param)
        {
            try
            {
                if ((param as PasswordBox).Password == await PLService.GetAdministorPasssword())
                {
                    LoginScreen.MyScreen = Screen.ADMINISTOR;
                }
                else
                {
                    MessageBox.Show("Incorrect Password", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    (param as PasswordBox).Password = string.Empty;
                }
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Customer login
        /// </summary>
        /// <param name="param"></param>
        public async void CustomerLogin(object param)
        {
            try
            {
                Customer customer = await PLService.GetCustomer((int)customerLogin.Id);
                if (await PLService.IsNotActiveCustomer(customer.Id))
                    MessageBox.Show("Deleted customer");
                else
                {
                    LoginScreen.Id = customer.Id;
                    LoginScreen.MyScreen = Screen.CUSTOMER;
                }
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show($"Incorrect Id:{(int)customerLogin.Id}", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                customerLogin.Id = null;
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

