using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for ParcelToListWindow.xaml
    /// </summary>
    public partial class ParcelToListWindow : UserControl
    {
        public ParcelToListWindow()
        {
            InitializeComponent();
        }
        public ParcelToListWindow(object Id, object State)
        {
            InitializeComponent();
            DataContext = new ParcelToListVM(Id, State);
        }
    }
}
