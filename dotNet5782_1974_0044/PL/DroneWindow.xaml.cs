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
        //private Visibility collapsed = Visibility.Collapsed;

        //add new drone
        public Drone()
        {
            InitializeComponent();
            //DroneToList drone = new();
            details.Visibility = Visibility.Collapsed;
            DataContext = Enum.GetValues(typeof(WeightCategories));
            station.DataContext = bl.GetStaionsWithEmptyChargeSlots((int num)=>num >0).ToList().Select(station => station.Id);
        }
        //
        public Drone(IBL.BO.DroneToList drone)
        {
            
            InitializeComponent();
            add.Visibility = Visibility.Collapsed;
            DataContext = drone;
        }

        private void AddDrone_click(object sender, RoutedEventArgs e)
        {
            
            WeightCategories maxWeight = (WeightCategories)weigth.SelectedIndex;
            string droneModel = model.Text;
            int stationId =(int)station.SelectedValue;

           //bl.AddDrone(
           // new   
           //    )
        }
    }
}
