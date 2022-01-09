using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL
{

    public class UpdateDroneVM : DependencyObject
    {
        public RelayCommand OpenParcelCommand { get; set; }
        public RelayCommand OpenCustomerCommand { get; set; }

        private int id;
        public PO.Drone drone
        {
            get { return (PO.Drone)GetValue(droneProperty); }
            set { SetValue(droneProperty, value); }
        }

        public static readonly DependencyProperty droneProperty =
            DependencyProperty.Register("drone", typeof(PO.Drone), typeof(UpdateDroneVM), new PropertyMetadata(new PO.Drone()));
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

        public UpdateDroneVM(int id)
        {
            this.id = id;
            InitThisDrone();
            droneModel = drone.Model;
            UpdateDroneCommand = new(UpdateModel, param => drone.Error == null);
            ChargingDroneCommand = new(sendToCharging, param => drone.Error == null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => drone.Error == null);
            DeleteDroneCommand = new(DeleteDrone, param => drone.Error == null);
            DelegateVM.Drone += InitThisDrone;
            OpenParcelCommand = new(Tabs.OpenDetailes, null);
            OpenCustomerCommand = new(Tabs.OpenDetailes, null);
        }
        public void InitThisDrone()
        {
            drone = PLService.GetDrone(id);
        }
        public void UpdateModel(object param)
        {
            try
            {
                if (droneModel != drone.Model)
                {
                    PLService.UpdateDrone(drone.Id, drone.Model);
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
            DelegateVM.Drone?.Invoke();
        }

        public void sendToCharging(object param)
        {
            if (drone.DroneState == PO.DroneState.AVAILABLE)
            {
                PLService.SendDroneForCharg(drone.Id);
                DelegateVM.Drone?.Invoke();
                DelegateVM.Station?.Invoke();
            }
            else if (drone.DroneState == PO.DroneState.MAINTENANCE)
            {
                PLService.ReleaseDroneFromCharging(drone.Id);
                DelegateVM.Drone?.Invoke();
                DelegateVM.Station?.Invoke();
            }
        }
        public void parcelTreatedByDrone(object param)
        {
            try
            {
                if (drone.DroneState == PO.DroneState.DELIVERY)
                {
                    if (drone.Parcel.ParcelState == true)
                    {
                        PLService.DeliveryParcelByDrone(drone.Id);
                        DelegateVM.Drone?.Invoke();
                    }
                    else
                    {
                        PLService.ParcelCollectionByDrone(drone.Id);
                        DelegateVM.Drone?.Invoke();
                    }
                }
                else
                {
                    PLService.AssingParcelToDrone(drone.Id);
                    DelegateVM.Drone?.Invoke();

                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public void DeleteDrone(object param)
        {
            try
            {
                if (MessageBox.Show("You're sure you want to delete this drone?", "Delete Drone", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    PLService.DeleteDrone(drone.Id);
                    MessageBox.Show("The drone was successfully deleted");
                    Tabs.CloseTab((param as TabItemFormat).Header);
                    DelegateVM.Drone -= InitThisDrone;
                    DelegateVM.Drone?.Invoke();
                }
            }

            catch (BO.ThereAreAssociatedOrgansException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }      
    }
}
