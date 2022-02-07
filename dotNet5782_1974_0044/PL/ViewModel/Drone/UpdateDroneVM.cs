using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class UpdateDroneVM : DependencyObject
    {
        private int id;
        BackgroundWorker simulatorWorker;
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

        public RelayCommand OpenParcelCommand { get; set; }
        public RelayCommand OpenCustomerCommand { get; set; }
        public RelayCommand UpdateDroneCommand { get; set; }
        public RelayCommand CloseDroneCommand { get; set; }
        public RelayCommand ChargingDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }
        public RelayCommand DeleteDroneCommand { get; set; }
        public RelayCommand SimulatorCommand { get; set; }


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
            SimulatorCommand = new(StartSimulator);
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
                    //DelegateVM.NotifyStationChanged();
                }
                else if (drone.DroneState == PO.DroneState.MAINTENANCE)
                {
                    PLService.ReleaseDroneFromCharging(drone.Id);
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    //DelegateVM.NotifyStationChanged();
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


        public static readonly DependencyProperty AutoProperty =
            DependencyProperty.Register(nameof(Auto), typeof(bool), typeof(UpdateDroneVM), new PropertyMetadata(false));
        public bool Auto
        {
            get => (bool)GetValue(AutoProperty);
            set { 
                SetValue(AutoProperty, value);
                if (value)
                    StartSimulator(null);
                else
                    StopSimulator(null);
            }
        }
        private void StartSimulator(object param)
        {
            Auto = true;
            SimulatorCommand = new(StopSimulator);
            simulatorWorker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
             simulatorWorker.DoWork += (sender, args) => PLService.StartDroneSimulator(id, updateDrone, IsSimulatorStoped);
            simulatorWorker.RunWorkerCompleted += (sender, args) => Auto = false;
            simulatorWorker.ProgressChanged += HandleWorkerProgressChanged;
            simulatorWorker.RunWorkerAsync(id);
        }

        private void HandleWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DelegateVM.NotifyDroneChanged(id);
        }

        private void StopSimulator(object param)
        {
            SimulatorCommand = new(StartSimulator);
            simulatorWorker?.CancelAsync();
        }
        private void updateDrone() => simulatorWorker.ReportProgress(0);
        private bool IsSimulatorStoped() => simulatorWorker.CancellationPending;
    }
}
