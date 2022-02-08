using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class UpdateDroneVM : DependencyObject
    {
        private int id;
        BackgroundWorker simulatorWorker;
        Action simulateDrone;
       
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
            DelegateVM.DroneChangedEvent += HandleADroneChanged;
            OpenParcelCommand = new(Tabs.OpenDetailes, null);
            OpenCustomerCommand = new(Tabs.OpenDetailes, null);
            simulateDrone = StartSimulator;
            SimulatorCommand = new((param) => simulateDrone());
        }
        private void HandleADroneChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitThisDrone();
        }
        public void InitThisDrone()
        {
            try
            {
                drone = PLService.GetDrone(id);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
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
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
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
                    DelegateVM.NotifyStationChanged();
                }
                else if (drone.DroneState == PO.DroneState.MAINTENANCE)
                {
                    PLService.ReleaseDroneFromCharging(drone.Id);
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    DelegateVM.NotifyStationChanged();
                }
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException)
            {
                MessageBox.Show("no available station");
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show($"{ex.Id} {ex.Message}");
            }
            catch (BO.InvalidDroneStateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.TheDroneIsNotInChargingException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
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
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.InvalidParcelStateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
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
                    DelegateVM.DroneChangedEvent -= HandleADroneChanged;
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    Tabs.CloseTab(param as TabItemFormat);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.InvalidDroneStateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.TheDroneIsNotInChargingException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
        #region simulator
        public static readonly DependencyProperty AutoProperty =
            DependencyProperty.Register(nameof(Auto), typeof(bool), typeof(UpdateDroneVM), new PropertyMetadata(false));
        public bool Auto
        {
            get => (bool)GetValue(AutoProperty);
            set => SetValue(AutoProperty, value);
        }
        private void StartSimulator()
        {
            Auto = true;
            simulateDrone = StopSimulator;
            simulatorWorker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
            simulatorWorker.DoWork += (sender, args) => PLService.StartDroneSimulator(id, updateDrone, IsSimulatorStoped);
            simulatorWorker.RunWorkerCompleted += (sender, args) => Auto = false;
            simulatorWorker.ProgressChanged += HandleWorkerProgressChanged;
            simulatorWorker.RunWorkerAsync(id);
        }
        private void HandleWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DelegateVM.NotifyDroneChanged(id);
            var ids = (ValueTuple<int?, int?, int?, int?>)e.UserState;
            if (ids.Item1 != null)
                DelegateVM.NotifyParcelChanged(ids.Item1);
            if (ids.Item2 != null)
                DelegateVM.NotifyCustomerChanged(ids.Item2);
            if (ids.Item3 != null)
                DelegateVM.NotifyCustomerChanged(ids.Item3);
            if (ids.Item4 != null)
                DelegateVM.NotifyStationChanged(ids.Item4);

        }
        private void StopSimulator()
        {
            simulateDrone = StartSimulator;
            simulatorWorker?.CancelAsync();
        }
        private void updateDrone(int? parcelId, int? senderId, int? receiverId, int? stationId)
        {
            simulatorWorker.ReportProgress(0, (parcelId, senderId, receiverId, stationId));
        }
        private bool IsSimulatorStoped() => simulatorWorker.CancellationPending;
        #endregion
    }
}
