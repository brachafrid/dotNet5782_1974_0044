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
using IBL.BO;
using IBL;
using Utilities;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : UserControl
    {
        IBL.IBL bl = Singletone<BL>.Instance;
        //add new drone
        public Drone()
        {
            InitializeComponent();
            //DroneToList drone = new();
            DataContext = Enum.GetValues(typeof(WeightCategories));
        }
        //
        public Drone(DroneToList drone)
        {
            InitializeComponent();
        }

        private void AddDrone_click(object sender, RoutedEventArgs e)
        {
            UserControl userControl = (sender as Button).Parent as UserControl;
            WeightCategories maxweight = (WeightCategories)weigth.SelectedIndex;
            string dronemodel = model
        }
    }
}
