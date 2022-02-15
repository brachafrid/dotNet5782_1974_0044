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
        public CustomerAddVM Add { get; set; }
        public RelayCommand AdministratorLoginCommand { get; set; }
        public RelayCommand CustomerLoginCommand { get; set; }
        public RelayCommand ShowAdministratorLoginCommand { get; set; }
        public RelayCommand ShowCustomerLoginCommand { get; set; }
        public RelayCommand ShowCustomerSigninCommand { get; set; }
        public CustomerLogin customerLogin { get; set; } = new();
        private Visibility visibilityAdministratorLogin = Visibility.Collapsed;

        public Visibility VisibilityAdministratorLogin
        {
            get => visibilityAdministratorLogin; 
            set => Set(ref visibilityAdministratorLogin, value);
        }

        private Visibility visibilityCustomerLogin = Visibility.Collapsed;

        public Visibility VisibilityCustomerLogin
        {
            get =>  visibilityCustomerLogin; 
            set => Set(ref visibilityCustomerLogin, value);
        }


        private Visibility visibilityCustomerSignIn = Visibility.Collapsed;

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
                    MessageBox.Show("Incorrect Password");
                    (param as PasswordBox).Password = string.Empty;
                }
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
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
                MessageBox.Show("Incorrect Id");
                customerLogin.Id = null;
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
    }
}

