using PL.PO;
using System.Collections.Generic;
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
            state = LoginState.CLOSE;
            ShowCommand = new RelayCommand(Show, null);
            Command = new RelayCommand(Log,(param)=> customerLogin.Error == null|| state != LoginState.CUSTOMER );
        }
        private LoginState state;
        public LoginState State
        {
            get { return state; }
            set { Set(ref state, value); }
        }
        /// <summary>
        /// Customer Add view model
        /// </summary>
        public CustomerAddVM Add { get; set; }
        public RelayCommand Command { get; set; }
        /// <summary>
        /// Command of showing administrator login
        /// </summary>
        public RelayCommand ShowCommand { get; set; }
        /// <summary>
        /// Command of showing customer login
        /// </summary>
        public CustomerLogin customerLogin { get; set; } = new();
        /// <summary>
        /// Show administrator login
        /// </summary>
        /// <param name="param"></param>
        public void Show(object param)
        {
            State = (LoginState)param;
        }
        /// <summary>
        /// login
        /// </summary>
        /// <param name="param"></param>  
        public void Log(object param)
        {
            switch (State)
            {
                case LoginState.ADMINISTOR:
                    AdministratorLogin(param);
                    break;
                case LoginState.CUSTOMER:
                    CustomerLogin(param);
                    break;
                case LoginState.SIGNIN:
                    break;
                default:
                    break;
            }
        }
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
                if (customer.Phone != customerLogin.Phone)
                {
                    MessageBox.Show("Incorrcet phone or Id", "Login Customer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (await PLService.IsNotActiveCustomer(customer.Id))
                    MessageBox.Show($"The customer {customer.Id} is deleted", "Login Customer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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