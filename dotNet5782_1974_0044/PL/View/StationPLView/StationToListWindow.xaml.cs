using System;
using PL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationToListWindow.xaml
    /// </summary>
    public partial class StationToListWindow : UserControl
    {
        private MainWindow mainWindow;
        public StationToListWindow()
        {
            InitializeComponent();
            mainWindow = MainWindow.GetWindow(this) as MainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
