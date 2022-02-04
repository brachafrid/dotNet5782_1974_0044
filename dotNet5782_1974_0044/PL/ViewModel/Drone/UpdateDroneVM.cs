using PL.PO;
using System;
using System.Collections.Generic;
using System.Windows;

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
            ChargingDroneCommand = new(SendToCharging, param => drone.Error == null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => drone.Error == null);
            DeleteDroneCommand = new(DeleteDrone, param => drone.Error == null);
            DelegateVM.DroneChangedEvent += HandleDroneChanged;
            OpenParcelCommand = new(Tabs.OpenDetailes, null);
            OpenCustomerCommand = new(Tabs.OpenDetailes, null);
        }

        private void HandleDroneChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id)
                InitThisDrone();
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
            DelegateVM.NotifyDroneChanged(drone.Id);
        }

        public void SendToCharging(object param)
        {
            try
            {
                if (drone.DroneState == PO.DroneState.AVAILABLE)
                {
                    PLService.SendDroneForCharg(drone.Id);
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    DelegateVM.Station?.Invoke();
                }
                else if (drone.DroneState == PO.DroneState.MAINTENANCE)
                {
                    PLService.ReleaseDroneFromCharging(drone.Id);
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    DelegateVM.Station?.Invoke();
                }
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException)
            {
                MessageBox.Show("no available station");
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
                        DelegateVM.NotifyDroneChanged(drone.Id);
                    }
                    else
                    {
                        PLService.ParcelCollectionByDrone(drone.Id);
                        DelegateVM.NotifyDroneChanged(drone.Id);
                    }
                }
                else
                {
                    PLService.AssingParcelToDrone(drone.Id);
                    DelegateVM.NotifyDroneChanged(drone.Id);

                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public void DeleteDrone(object param)
        {

            if (MessageBox.Show("You're sure you want to delete this drone?", "Delete Drone", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                PLService.DeleteDrone(drone.Id);
                MessageBox.Show("The drone was successfully deleted");
                DelegateVM.DroneChangedEvent -= HandleDroneChanged;
                DelegateVM.NotifyDroneChanged(drone.Id);
                Tabs.CloseTab(param as TabItemFormat);
            }
        }
    }
}
