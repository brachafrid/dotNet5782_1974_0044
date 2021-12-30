using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class UpdateDroneVM: DependencyObject
    {
        public Drone drone
        {
            get { return (Drone)GetValue(droneProperty); }
            set { SetValue(droneProperty, value); }
        }

        public static readonly DependencyProperty droneProperty =
            DependencyProperty.Register("drone", typeof(Drone), typeof(UpdateDroneVM), new PropertyMetadata(new Drone()));
        public string droneModel
        {
            get { return (string)GetValue(droneModelProperty); }
            set { SetValue(droneModelProperty, value); }
        }

        public static readonly DependencyProperty droneModelProperty =
            DependencyProperty.Register("droneModel", typeof(string), typeof(UpdateDroneVM), new PropertyMetadata(string.Empty));


        public RelayCommand UpdateDroneCommand { get; set; }
        public RelayCommand CloseDroneCommand { get; set; }
        public RelayCommand ChargingDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }
        public RelayCommand DeleteDroneCommand { get; set; }
        
        public UpdateDroneVM()
        {
            init();
            droneModel = drone.Model;
            UpdateDroneCommand = new(UpdateModel, param => drone.Error == null);
            ChargingDroneCommand = new(sendToCharging, param => drone.Error == null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => drone.Error == null);
            DeleteDroneCommand = new(DeleteDrone, param => drone.Error == null);
            DelegateVM.Drone += init; 
        }
        public void init()
        {
            drone = new DroneHandler().GetDrone(2);
        }
        public void UpdateModel(object param)
        {
            try
            {
                if (droneModel != drone.Model)
                {
                    new DroneHandler().UpdateDrone(drone.Id, drone.Model);
                    MessageBox.Show("The drone has been successfully updated");
                    droneModel = drone.Model;

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
            DelegateVM.Drone();
        }

        public void sendToCharging(object param)
        {
            if (drone.DroneState == DroneState.AVAILABLE)
            {
                new DroneHandler().SendDroneForCharg(drone.Id);
                DelegateVM.Drone();


                MessageBox.Show($"{drone.DroneState}");
            }
            else if (drone.DroneState == DroneState.MAINTENANCE)
            {
                new DroneHandler().ReleaseDroneFromCharging(drone.Id);
                DelegateVM.Drone();

                MessageBox.Show($"{drone.DroneState}");

            }
        }


        public void parcelTreatedByDrone(object param)
        {
            try { 
                if (drone.DroneState == DroneState.DELIVERY)
                {
                    if (drone.Parcel.ParcelState == true)
                    {
                        new DroneHandler().DeliveryParcelByDrone(drone.Id);
                        DelegateVM.Drone();

                    }
                    else
                    {
                        new DroneHandler().ParcelCollectionByDrone(drone.Id);
                        DelegateVM.Drone();

                    }

                }
                else
                {
                    new DroneHandler().AssingParcelToDrone(drone.Id);
                    DelegateVM.Drone();

                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public void DeleteDrone(object param)
        {
            new DroneHandler().DeleteDrone(drone.Id);
        }

    }
}
