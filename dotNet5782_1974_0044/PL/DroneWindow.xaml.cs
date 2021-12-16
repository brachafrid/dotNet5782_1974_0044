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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>

    public partial class DroneWindow : UserControl
    {
        IBL.IBL bl = Singletone<BL>.Instance;
        //private Visibility collapsed = Visibility.Collapsed;
        //add new drone
        public DroneWindow()
        {
            InitializeComponent();
            //DroneToList drone = new();
            details.Visibility = Visibility.Collapsed;
            DataContext = Enum.GetValues(typeof(WeightCategories));
            station.DataContext = bl.GetStaionsWithEmptyChargeSlots((int num) => num > 0).ToList().Select(station => station.Id);
        }

        public DroneWindow(IBL.BO.DroneToList droneToList)
        {
            InitializeComponent();
            add.Visibility = Visibility.Collapsed;
            var drone = bl.GetDrone(droneToList.Id);
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
                Close(sender, e);
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
            object tmp = sender;
            while (tmp.GetType() != typeof(MainWindow))
                tmp = ((FrameworkElement)tmp).Parent;
            
            MainWindow mainWindow = (MainWindow)tmp;
           
            mainWindow.Close_tab(sender, e);
        }
        private void UpdateDrone(object sender, RoutedEventArgs e)
        {

            Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try 
            {
                bl.UpdateDrone(drone.Id, drone.Model);
                MessageBox.Show("The drone has been successfully updated");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
                MessageBox.Show("For updating the name must be initialized ");
            }
        }
  
        private void SendToCharging(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone droneToList = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.SendDroneForCharg(droneToList.Id);
                MessageBox.Show("The drone was sent for loading successfully");
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show( ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
             //   MessageBox.Show("The drone is not available so it is not possible to send it for charging");
            }
        }

        private void ReleaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            timeCharge.Visibility = Visibility.Visible;
            timeOfCharge.Visibility = Visibility.Visible;
            confirm.Visibility = Visibility.Visible;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone droneToList = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;

            try
            {
                float timeCrg = float.Parse(timeOfCharge.Text);
                bl.ReleaseDroneFromCharging(droneToList.Id, timeCrg);
                MessageBox.Show("The drone was successfully released from the charging");
                timeCharge.Visibility = Visibility.Collapsed;
                timeOfCharge.Visibility = Visibility.Collapsed;
                confirm.Visibility = Visibility.Collapsed;

            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("For updating the name must be initialized ");
            }
        }

        private void AssingParcelToDrone(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone droneToList = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.AssingParcelToDrone(droneToList.Id);
                MessageBox.Show("The drone was successfully shipped");
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
                //MessageBox.Show(" The drone is not aviable so it is not possible to assign it a parcel");
            }
        }

        private void ParcelCollectionByDrone(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone droneToList = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.ParcelCollectionByDrone(droneToList.Id);
                MessageBox.Show("The parcel was successfully collected");
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }

            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
              
                    //MessageBox.Show("No parcel has been associated yet");
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");

               // MessageBox.Show("The drone is not in delivery");
            }
        }

        private void DeliveryParcelByDrone(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone droneToList = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.DeliveryParcelByDrone(droneToList.Id);
                MessageBox.Show("The drone was successfully shipped");
            }
            catch(KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show( ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
               // MessageBox.Show(" The drone is not aviable so it is not possible to assign it a parcel");
            }
        }

        private void Buttons(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone droneToList = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            if (droneToList.DroneState == DroneState.AVAILABLE)
            {
                sendToCharging.Visibility = Visibility.Visible;
                assingParcelToDrone.Visibility = Visibility.Visible;
            }
            if (droneToList.DroneState == DroneState.MAINTENANCE)
            {
                releaseDroneFromCharging.Visibility = Visibility.Visible;
            }
            if (droneToList.DroneState == DroneState.DELIVERY)
            {
                parcelCollectionByDrone.Visibility = Visibility.Visible;
                deliveryParcelByDrone.Visibility = Visibility.Visible;
            }

        }
     
    }
}
