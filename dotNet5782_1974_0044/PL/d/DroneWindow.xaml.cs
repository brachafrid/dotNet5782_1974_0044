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
        private string modelNew;
        private Action updateList;
        private MainWindow MainWindow;
        //private Visibility collapsed = Visibility.Collapsed;
        //add new drone
        public DroneWindow(Action updateListNew,MainWindow mainWindow)
        {
            InitializeComponent();
            //DroneToList drone = new();
            details.Visibility = Visibility.Collapsed;
            DataContext = Enum.GetValues(typeof(WeightCategories));
            station.DataContext = bl.GetStaionsWithEmptyChargeSlots((int num) => num > 0).ToList().Select(station => station.Id);
            updateList = updateListNew;
            MainWindow = mainWindow;
        }

        public DroneWindow(IBL.BO.DroneToList droneToList,Action updateListNew,MainWindow mainWindow)
        {
            InitializeComponent();
            add.Visibility = Visibility.Collapsed;
            var drone = bl.GetDrone(droneToList.Id);
            DataContext = drone;
            modelNew = drone.Model;
            updateList = updateListNew;
            MainWindow = mainWindow;
        }
      
        private void ValidDetails(object sender, RoutedEventArgs e)
        {
            Valid_Id_Drone(sender, e);
            Valid_Model_Drone(sender, e);
            Valid_Weight_Drone(sender, e);
            Valid_Station_Drone(sender, e);
        }

        private void Valid_Id_Drone(object sender, RoutedEventArgs e)
        {
            is_num(sender, (TextChangedEventArgs)e);

            if (id.Text == string.Empty)
            {
                id.Background = Brushes.OrangeRed;
                MessageBox.Show("enter id");
                //return false;
            }
            //return true;
        }
        private void Valid_Model_Drone(object sender, RoutedEventArgs e)
        {
            string droneModel = model.Text;
            if (droneModel == string.Empty)
            {
                model.Background = Brushes.OrangeRed;

                MessageBox.Show("enter model");
                //return false;
            }
            //return true;
        }
        private void Valid_Weight_Drone(object sender, RoutedEventArgs e)
        {
            WeightCategories maxWeight = (WeightCategories)weigth.SelectedIndex;
            if ((int)maxWeight == -1)
            {
                //model = "red";
                weigth.Background = Brushes.OrangeRed;

                MessageBox.Show("choose max weigth");
                //return false;
            }
            //return true;
        }
        private void Valid_Station_Drone(object sender, RoutedEventArgs e)
        {
            if (station.SelectedIndex == -1)
            {
                station.Background = Brushes.OrangeRed;
                MessageBox.Show("choose station");
                //return false;
            }
            //return true;
        }

        private void AddDrone_click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            WeightCategories maxWeight = (WeightCategories)weigth.SelectedIndex;
            string droneModel = model.Text;

            if (droneModel == string.Empty)
            {
                model.Background = Brushes.OrangeRed;
                MessageBox.Show("enter model");

                valid = false;
            }
            if (id.Text == string.Empty)
            {
                id.Background = Brushes.OrangeRed;
                MessageBox.Show("enter id");
                valid = false;

                //return false;
            }
            if ((int)maxWeight == -1)
            {
                //model = "red";
                weigth.Background = Brushes.OrangeRed;
                valid = false;

                MessageBox.Show("choose max weigth");
                //return false;
            }
            if (station.SelectedIndex == -1)
            {
                station.Background = Brushes.OrangeRed;
                valid = false;
                MessageBox.Show("choose station");
                //return false;
            }
           
            
            try
            {
                int stationId = 0;
                if (station.SelectedValue == null)
                {
                    //MessageBox.Show("choose station");
                    valid = false;
                }
                else
                {
                    stationId = (int)station.SelectedValue;
                }
                if (valid == true) {
                    id.Background = Brushes.GreenYellow;
                    model.Background = Brushes.GreenYellow;
                    weigth.Background = Brushes.GreenYellow;
                    station.Background = Brushes.GreenYellow;

                    bl.AddDrone(new IBL.BO.Drone()
                    {
                        Id = int.Parse(id.Text),
                        Model = droneModel,
                        WeightCategory = maxWeight
                    }, stationId);
                    updateList();
                    MessageBox.Show("add succses");
                    Close(sender, e);
                }
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("id is already exist");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("choose station");
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
            MainWindow.Close_tab(sender, e);
        }

        private void model_changed(object sender, RoutedEventArgs e)
        {
            modelNew = (e.OriginalSource as TextBox).Text;
        }

        private void UpdateDrone(object sender, RoutedEventArgs e)
        {

            Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                if(modelNew!=drone.Model)
                {
                    bl.UpdateDrone(drone.Id, modelNew);
                    MessageBox.Show("The drone has been successfully updated");
                    updateList();
                }
                else
                {
                    MessageBox.Show("Model name not updated");
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
                MessageBox.Show("For updating the name must be initialized ");
            }
        }

        private void SendToCharging(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.SendDroneForCharg(drone.Id);
                DataContext = bl.GetDrone(drone.Id);
                MessageBox.Show("The drone was sent for loading successfully");
                updateList();
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
        }

        private void ReleaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            timeCharge.Visibility = Visibility.Visible;
            timeOfCharge.Visibility = Visibility.Visible;
            timeOfCharge.Text = "";
            confirm.Visibility = Visibility.Visible;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;

            try
            {
                float timeCrg = float.Parse(timeOfCharge.Text);
                bl.ReleaseDroneFromCharging(drone.Id, timeCrg);
                MessageBox.Show("The drone was successfully released from the charging");
                timeCharge.Visibility = Visibility.Collapsed;
                timeOfCharge.Visibility = Visibility.Collapsed;
                confirm.Visibility = Visibility.Collapsed;
                updateList();

                DataContext = bl.GetDrone(drone.Id);
            }
            catch (FormatException)
            {
                MessageBox.Show("enter minutes of charging");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("For updating the name must be initialized ");
            }
        }

        private void AssingParcelToDrone(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.AssingParcelToDrone(drone.Id);
                MessageBox.Show("The drone was successfully shipped");
                DataContext = bl.GetDrone(drone.Id);
                updateList();
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
        }

        private void ParcelCollectionByDrone(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.ParcelCollectionByDrone(drone.Id);
                MessageBox.Show("The parcel was successfully collected");
                DataContext = bl.GetDrone(drone.Id);
                updateList();
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }

            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");

            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
        }

        private void DeliveryParcelByDrone(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                bl.DeliveryParcelByDrone(drone.Id);
                MessageBox.Show("The drone was successfully shipped");
                DataContext = bl.GetDrone(drone.Id);
                updateList();
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
        }

        //private void Buttons(object sender, RoutedEventArgs e)
        //{
        //    IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
        //    if (drone.DroneState == DroneState.AVAILABLE)
        //    {
        //        sendToCharging.Visibility = Visibility.Visible;
        //        assingParcelToDrone.Visibility = Visibility.Visible;
        //    }
        //    if (drone.DroneState == DroneState.MAINTENANCE)
        //    {
        //        releaseDroneFromCharging.Visibility = Visibility.Visible;
        //    }
        //    if (drone.DroneState == DroneState.DELIVERY)
        //    {
        //        parcelCollectionByDrone.Visibility = Visibility.Visible;
        //        deliveryParcelByDrone.Visibility = Visibility.Visible;
        //    }

        //}

    }
}
