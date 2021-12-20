using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BO;
using BL;
using Utilities;
using System.ComponentModel;
using BLApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : UserControl
    {
        BLApi.IBL bl = BLFactory.GetBL();
        //private string modelNew;
        private Action updateList;
        private MainWindow MainWindow;
        ///// <summary>
        ///// Constructor for displaying and updating a drone
        ///// </summary>
        ///// <param name="updateListNew">Action - update list new</param>
        ///// <param name="mainWindow">The main window</param>
        public string modelNew
        {
            get { return (string)GetValue(modelNewProperty); }
            set { SetValue(modelNewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for modelNew.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty modelNewProperty =
            DependencyProperty.Register("modelNew", typeof(string), typeof(DroneWindow), new PropertyMetadata(string.Empty));


        /// <summary>
        /// Constructor for displaying and updating a drone
        /// </summary>
        /// <param name="updateListNew">Action - update list new</param>
        /// <param name="mainWindow">The main window</param>
        public DroneWindow(Action updateListNew, MainWindow mainWindow)
        {
            InitializeComponent();
            details.Visibility = Visibility.Collapsed;
            DataContext = Enum.GetValues(typeof(WeightCategories));
            station.DataContext = bl.GetStaionsWithEmptyChargeSlots((int num) => num > 0).ToList().Select(station => station.Id);
            updateList = updateListNew;
            MainWindow = mainWindow;
        }
        /// <summary>
        /// Constructor for adding a new skimmer
        /// </summary>
        /// <param name="droneToList">The drone for add</param>
        /// <param name="updateListNew">Action - update list new</param>
        /// <param name="mainWindow">The main window</param>

        public DroneWindow(BO.DroneToList droneToList, Action updateListNew, MainWindow mainWindow)
        {
            InitializeComponent();
            add.Visibility = Visibility.Collapsed;
            var drone = bl.GetDrone(droneToList.Id);
            DataContext = drone;
            modelNew = drone.Model;
            updateList = updateListNew;
            MainWindow = mainWindow;
        }

        /// <summary>
        /// Checking the correctness of the id with the addition of a new drone
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Valid_Id_Drone(object sender, RoutedEventArgs e)
        {
            is_num(sender, (TextChangedEventArgs)e);

            if (id.Text == string.Empty)
            {
                id.Background = Brushes.OrangeRed;
            }
            else
                id.Background = Brushes.GreenYellow;
        }
        /// <summary>
        /// Checking the correctness of the model with the addition of a new drone
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Valid_Model_Drone(object sender, RoutedEventArgs e)
        {
            string droneModel = model.Text;
            if (droneModel == string.Empty)
            {
                model.Background = Brushes.OrangeRed;
                MessageBox.Show("enter model");
            }
            else
                model.Background = Brushes.GreenYellow;
        }
        /// <summary>
        /// Checking the correctness of the weight with the addition of a new drone
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Valid_Weight_Drone(object sender, RoutedEventArgs e)
        {
            WeightCategories maxWeight = (WeightCategories)weigth.SelectedIndex;
            if ((int)maxWeight == -1)
            {
                weigth.Background = Brushes.OrangeRed;
                MessageBox.Show("choose max weigth");
            }
            else
                weigth.Background = Brushes.GreenYellow;
        }
        /// <summary>
        /// Checking the correctness of the station with the addition of a new drone
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Valid_Station_Drone(object sender, RoutedEventArgs e)
        {
            if (station.SelectedIndex == -1)
            {
                station.Background = Brushes.OrangeRed;
                MessageBox.Show("choose station");
            }
            else
                station.Background = Brushes.GreenYellow;
        }
        /// <summary>
        /// Adding a new drone.
        /// Checks if all data is valid, and add the new drone
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void AddDrone_click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            int stationId = 0;
            WeightCategories maxWeight = (WeightCategories)weigth.SelectedIndex;
            string droneModel = model.Text;

            if (id.Text == string.Empty)
            {
                id.Background = Brushes.OrangeRed;
                MessageBox.Show("enter id");
                valid = false;
            }

            if (droneModel == string.Empty)
            {
                model.Background = Brushes.OrangeRed;
                MessageBox.Show("enter model");
                valid = false;
            }

            if ((int)maxWeight == -1)
            {
                weigth.Background = Brushes.OrangeRed;
                valid = false;
                MessageBox.Show("choose max weigth");
            }

            if (station.SelectedIndex == -1)
            {
                station.Background = Brushes.OrangeRed;
                valid = false;
                MessageBox.Show("choose station");
            }

            if (station.SelectedValue == null)
                valid = false;
            else
                stationId = (int)station.SelectedValue;
            if (valid)
            {
                id.Background = Brushes.GreenYellow;
                model.Background = Brushes.GreenYellow;
                weigth.Background = Brushes.GreenYellow;
                station.Background = Brushes.GreenYellow;
                try
                {
                    bl.AddDrone(new BO.Drone()
                    {
                        Id = int.Parse(id.Text),
                        Model = droneModel,
                        WeightCategory = maxWeight
                    }, stationId);
                    updateList();
                    MessageBox.Show("add succses");
                    Close(sender, e);
                }
                catch (ThereIsAnObjectWithTheSameKeyInTheListException)
                {
                    id.Background = Brushes.OrangeRed;
                    MessageBox.Show("id is already exist");
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("choose station");
                }
            }
        }
        /// <summary>
        /// Checks if only digits are entered
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void is_num(object sender, TextChangedEventArgs e)
        {
            foreach (var item in id.Text)
                if (!char.IsDigit(item))
                {
                    id.Background = Brushes.OrangeRed;
                    MessageBox.Show("please enter positive number for id");
                    id.Text = "";
                    return;
                }
            id.Background = default;
        }
        /// <summary>
        /// Closes the tab
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void Close(object sender, RoutedEventArgs e)
        {
            MainWindow.Close_tab(sender, e);
        }

        /// <summary>
        /// Renames the model
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void model_changed(object sender, RoutedEventArgs e)
        {
            modelNew = (e.OriginalSource as TextBox).Text;
        }

        /// <summary>
        /// Updates the model name to the new name entered
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>

        private void UpdateDrone(object sender, RoutedEventArgs e)
        {

            Drone drone = (BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                if (modelNew != drone.Model)
                {
                    bl.UpdateDrone(drone.Id, modelNew);
                    MessageBox.Show("The drone has been successfully updated");
                    modelNew = drone.Model;
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


       
        private void TreatDroneCharging(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = (BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                if(drone.DroneState == DroneState.AVAILABLE) 
                { 
                    bl.SendDroneForCharg(drone.Id);
                    DataContext = bl.GetDrone(drone.Id);
                    MessageBox.Show("The drone was sent for loading successfully");
                    updateList();
                }
                else
                {
                    timeCharge.Visibility = Visibility.Visible;
                    timeOfCharge.Visibility = Visibility.Visible;
                    timeOfCharge.Text = "";
                    confirm.Visibility = Visibility.Visible;
                }
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show(ex.Message == string.Empty ? $"{ex}" : $"{ex.Message}");
            }
        }

        ///// <summary>
        ///// Releases the drone from charging -
        ///// Allows the user to enter the amount of loading minutes
        ///// </summary>
        ///// <param name="sender">Event operator</param>
        ///// <param name="e">The arguments of the event</param>
    
        ///// <summary>
        ///// Updates the loading minutes that the user has entered and releases from loading
        ///// </summary>
        ///// <param name="sender">Event operator</param>
        ///// <param name="e">The arguments of the event</param>
     
        private void Confirm(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = (BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
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

        /// <summary>
        /// Handling a package by a drone
        /// </summary>
        /// <param name="sender">Event operator</param>
        /// <param name="e">The arguments of the event</param>
        private void ParcelTreatedByDrone(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = (BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            try
            {
                if (drone.DroneState == DroneState.DELIVERY)
                {
                    if (!drone.Parcel.ParcelState)
                    {
                        bl.ParcelCollectionByDrone(drone.Id);
                        MessageBox.Show("The parcel was successfully collected");
                        DataContext = bl.GetDrone(drone.Id);
                        updateList();
                    }
                    else
                    {
                        bl.DeliveryParcelByDrone(drone.Id);
                        MessageBox.Show("The drone was successfully shipped");
                        DataContext = bl.GetDrone(drone.Id);
                        updateList();
                    }
                }
                else
                {
                    bl.AssingParcelToDrone(drone.Id);
                    MessageBox.Show("The drone was successfully shipped");
                    DataContext = bl.GetDrone(drone.Id);
                    updateList();
                }
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
    }
}
