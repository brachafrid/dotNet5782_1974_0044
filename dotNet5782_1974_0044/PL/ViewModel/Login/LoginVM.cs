﻿using System;
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
        public RelayCommand AdministratorLoginCommand { get; set; }
        public RelayCommand CustomerLoginCommand { get; set; }
        public RelayCommand ShowAdministratorLoginCommand { get; set; }
        public RelayCommand ShowCustomerLoginCommand { get; set; }
        public CustomerLogin customerLogin { get; set; } = new();

        public Visibility VisibilityAdministratorLogin
        {
            get { return (Visibility)GetValue(VisibilityAdministratorLoginProperty); }
            set { SetValue(VisibilityAdministratorLoginProperty, value); }
        }
        // Using a DependencyProperty as the backing store for VisibilityAdministratorLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityAdministratorLoginProperty =
            DependencyProperty.Register("VisibilityAdministratorLogin", typeof(Visibility), typeof(LoginVM), new PropertyMetadata(Visibility.Collapsed));

        public Visibility VisibilityCustomerLogin
        {
            get { return (Visibility)GetValue(VisibilityCustomerLoginProperty); }
            set { SetValue(VisibilityCustomerLoginProperty, value); }
        }
        // Using a DependencyProperty as the backing store for VisibilityCustomerLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityCustomerLoginProperty =
            DependencyProperty.Register("VisibilityCustomerLogin", typeof(Visibility), typeof(LoginVM), new PropertyMetadata(Visibility.Collapsed));
        public LoginVM()
        {
            ShowAdministratorLoginCommand = new RelayCommand(ShowAdministratorLogin, null);
            ShowCustomerLoginCommand = new RelayCommand(ShowCustomerLogin, null);
            AdministratorLoginCommand = new RelayCommand(AdministratorLogin, null);
            CustomerLoginCommand = new RelayCommand(CustomerLogin, null);
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
                Customer customer = new CustomerHandler().GetCustomer((int)customerLogin.Id);
                LoginScreen.MyScreen = "CustomerWindow";
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Incorrect Id");
                customerLogin.Id = null;
            }
        }

    }
}
