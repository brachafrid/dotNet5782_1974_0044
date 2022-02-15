using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace PL.PO
{
    public static class LoginScreen
    {
        private static string myScreen { get; set; }
        /// <summary>
        /// The current screen: Login, Administrator or Customer
        /// </summary>
        public static string MyScreen
        {
            get { return myScreen; }
            set
            {
                myScreen = value;
                NotifyStaticPropertyChanged("MyScreen");
            }
        }
        private static int? id = null;
        /// <summary>
        /// The current screen key
        /// </summary>
        public static int? Id
        {
            get { return id; }
            set {
                id = value;
                NotifyStaticPropertyChanged("Id");
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        static LoginScreen()
        {
            MyScreen = "LoginWindow";
        }
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
