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
            station.DataContext = bl.GetStaionsWithEmptyChargeSlots((int num) => num > 0).ToList().Select(station => station.Id);
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
            if ((int)maxWeight == -1)
            {
                MessageBox.Show("choose max weigth");
                return;
            }
            string droneModel = model.Text;
            if (droneModel == string.Empty)
            {
                MessageBox.Show("enter model");
                return;
            }
            if (station.SelectedIndex == -1)
            {
                MessageBox.Show("choose station");
                return;
            }
            if (id.Text == string.Empty)
            {
                MessageBox.Show("enter id");
                return;
            }
            int stationId = (int)station.SelectedValue;
            try
            {
                bl.AddDrone(new IBL.BO.Drone() 
                { 
                    Id = int.Parse(id.Text),
                    Model = droneModel, 
                    WeightCategory = maxWeight 
                }, stationId);

                MessageBox.Show("add succses");

            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("id is already exist");
            }


        }

        private void is_num(object sender, TextChangedEventArgs e)
        {
            foreach (var item in id.Text)
                if (!char.IsDigit(item))
                {
                    MessageBox.Show("please enter positive number for id");
                    id.Text = "";
                    break;
                }
        }

        private void Close(object sender, RoutedEventArgs e)
        {

        }
         private void UpdateDataContent(object sender)
        {
            object tmp = sender;
            while(tmp.GetType() != typeof(TabControl))
                tmp = (tmp as FrameworkElement).Parent;
            foreach( var item in (tmp as TabControl).Items)
            {

            }
            

        }
    }
}
