﻿using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class UpdateDroneVM : INotifyPropertyChanged
    {
        private int id;
        BackgroundWorker simulatorWorker;
        Action simulateDrone;

        private Drone drone;
        public Drone Drone
        {
            get { return drone; }
            set
            {
                drone = value;
                onPropertyChanged("Drone");
            }
        }

        private string droneModel;
        public string DroneModel
        {
            get { return droneModel; }
            set
            {
                droneModel = value;
                onPropertyChanged("DroneModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
        public RelayCommand OpenParcelCommand { get; set; }
        public RelayCommand OpenCustomerCommand { get; set; }
        public RelayCommand UpdateDroneCommand { get; set; }
        public RelayCommand ChargingDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }
        public RelayCommand DeleteDroneCommand { get; set; }
        public RelayCommand SimulatorCommand { get; set; }
        public UpdateDroneVM(int id)
        {
            this.id = id;
            InitThisDrone();
            UpdateDroneCommand = new(UpdateModel, param => Drone?.Error == null);
            ChargingDroneCommand = new(SendToCharging, param => Drone?.Error == null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => Drone?.Error == null);
            DeleteDroneCommand = new(DeleteDrone, param => Drone?.Error == null);
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
        public async void InitThisDrone()
        {
            try
            {
                Drone = await PLService.GetDrone(id);
                droneModel = Drone.Model;
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
        public async void UpdateModel(object param)
        {
            try
            {
                if (droneModel != Drone.Model)
                {
                    await PLService.UpdateDrone(Drone.Id, Drone.Model);
                    MessageBox.Show("The drone has been successfully updated");
                    droneModel = Drone.Model;
                    DelegateVM.NotifyDroneChanged(drone.Id);

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
        public async void SendToCharging(object param)
        {
            try
            {
                if (Drone.DroneState == DroneState.AVAILABLE)
                {
                    await PLService.SendDroneForCharg(Drone.Id);
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    DelegateVM.NotifyStationChanged();
                }
                else if (Drone.DroneState == PO.DroneState.MAINTENANCE)
                {
                    await PLService.ReleaseDroneFromCharging(Drone.Id);
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
        public async void parcelTreatedByDrone(object param)
        {
            try
            {
                if (Drone.DroneState == PO.DroneState.DELIVERY)
                {
                    if (Drone.Parcel.ParcelState == true)
                    {
                        await PLService.DeliveryParcelByDrone(Drone.Id);
                        DelegateVM.NotifyDroneChanged(drone.Id);
                    }
                    else
                    {
                        await PLService.ParcelCollectionByDrone(Drone.Id);
                        DelegateVM.NotifyDroneChanged(drone.Id);
                    }
                }
                else
                {
                    
                    await PLService.AssingParcelToDrone(Drone.Id);
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    
                }
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
            catch (BO.NotExsistSuitibleStationException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
        public async void DeleteDrone(object param)
        {
            try
            {
                if (MessageBox.Show("You're sure you want to delete this drone?", "Delete Drone", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    await PLService.DeleteDrone(Drone.Id);
                    MessageBox.Show("The drone was successfully deleted");
                    DelegateVM.DroneChangedEvent -= HandleADroneChanged;
                    DelegateVM.NotifyDroneChanged(Drone.Id);
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

                if (simulatorWorker != null && !simulatorWorker.CancellationPending)
                    simulatorWorker.CancelAsync();
                if (simulatorWorker != null && !simulatorWorker.IsBusy)
                {
                    await PLService.DeleteDrone(Drone.Id);
                    MessageBox.Show("The drone was successfully deleted");
                    DelegateVM.DroneChangedEvent -= HandleADroneChanged;
                    DelegateVM.NotifyDroneChanged(drone.Id);
                    Tabs.CloseTab(param as TabItemFormat);
                }


            }
        }
        #region simulator
        private bool auto;

        public bool Auto
        {
            get { return auto; }
            set
            {
                auto = value;
                onPropertyChanged("Auto");
            }
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
            else if (ids.Item2 != null)
                DelegateVM.NotifyCustomerChanged(ids.Item2);
            else if (ids.Item3 != null)
                DelegateVM.NotifyCustomerChanged(ids.Item3);
            else if (ids.Item4 != null)
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
