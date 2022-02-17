using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class UpdateDroneVM : NotifyPropertyChangedBase, IDisposable
    {
        private int id;
        BackgroundWorker simulatorWorker;
        Action simulateDrone;

        private Drone drone;
        /// <summary>
        /// drone
        /// </summary>
        public Drone Drone
        {
            get { return drone; }
            set
            {
                Set(ref drone, value);
                drone = value;
            }
        }

        private string droneModel;
        /// <summary>
        /// drone model
        /// </summary>
        public string DroneModel
        {
            get { return droneModel; }
            set
            {
                Set(ref droneModel, value);
            }
        }
        /// <summary>
        /// Command of openning parcel
        /// </summary>
        public RelayCommand OpenParcelCommand { get; set; }
        /// <summary>
        /// Command of openning customer
        /// </summary>
        public RelayCommand OpenCustomerCommand { get; set; }
        /// <summary>
        /// Command of updating drone
        /// </summary>
        public RelayCommand UpdateDroneCommand { get; set; }
        /// <summary>
        /// Command of charging drone
        /// </summary>
        public RelayCommand ChargingDroneCommand { get; set; }
        /// <summary>
        /// Command of treating parcel by drone
        /// </summary>
        public RelayCommand ParcelTreatedByDrone { get; set; }
        /// <summary>
        /// Command of deleting drone
        /// </summary>
        public RelayCommand DeleteDroneCommand { get; set; }
        /// <summary>
        /// Command of simulator
        /// </summary>
        public RelayCommand SimulatorCommand { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id of drone</param>
        public UpdateDroneVM(int id)
        {
            this.id = id;
            InitThisDrone();
            UpdateDroneCommand = new(UpdateModel, param => Drone?.Error == null);
            ChargingDroneCommand = new(SendToCharging, param => Drone?.Error == null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => Drone?.Error == null);
            DeleteDroneCommand = new(DeleteDrone, param => Drone?.Error == null);
            RefreshEvents.DroneChangedEvent += HandleADroneChanged;
            OpenParcelCommand = new(Tabs.OpenDetailes, null);
            OpenCustomerCommand = new(Tabs.OpenDetailes, null);
            simulateDrone = StartSimulator;
            SimulatorCommand = new((param) => simulateDrone());
        }

        /// <summary>
        /// Handle drone changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void HandleADroneChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitThisDrone();
        }

        /// <summary>
        /// Initialize this drone
        /// </summary>
        public async void InitThisDrone()
        {
            try
            {
                Drone = await PLService.GetDrone(id);
                droneModel = Drone.Model;
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message,$"Init Drone Id: {id}",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Init Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Update model of drone
        /// </summary>
        /// <param name="param"></param>
        public async void UpdateModel(object param)
        {
            try
            {
                if (droneModel != Drone.Model)
                {
                    await PLService.UpdateDrone(Drone.Id, Drone.Model);
                    MessageBox.Show("The drone has been successfully updated","Update Drone",MessageBoxButton.OK,MessageBoxImage.Information);
                    droneModel = Drone.Model;
                    RefreshEvents.NotifyDroneChanged(drone.Id);

                }
                else
                {
                    MessageBox.Show("Model name not updated","Update Model",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message+Environment.NewLine+"For updating the name must be initialized ", $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            RefreshEvents.NotifyDroneChanged(drone.Id);
        }

        /// <summary>
        /// Send drone to charging
        /// </summary>
        /// <param name="param"></param>
        public async void SendToCharging(object param)
        {
            try
            {
                if (Drone.DroneState == DroneState.AVAILABLE)
                {
                    await PLService.SendDroneForCharg(Drone.Id);
                    RefreshEvents.NotifyDroneChanged(drone.Id);
                    RefreshEvents.NotifyStationChanged();
                }
                else if (Drone.DroneState == PO.DroneState.MAINTENANCE)
                {
                    await PLService.ReleaseDroneFromCharging(Drone.Id);
                    RefreshEvents.NotifyDroneChanged(drone.Id);
                    RefreshEvents.NotifyStationChanged();
                }
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show(ex.Message==string.Empty?ex.ToString():ex.Message, $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show($"{ex.Id} {ex.Message}", $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (BO.InvalidDroneStateException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.TheDroneIsNotInChargingException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BO.NotExsistSutibleParcelException )
            {
                MessageBox.Show("The drone is in the station but not in charge", "Sending the drone to charge", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// parcel treated by drone
        /// </summary>
        /// <param name="param"></param>
        public async void parcelTreatedByDrone(object param)
        {
            try
            {
                if (Drone.DroneState == PO.DroneState.DELIVERY)
                {
                    if (Drone.Parcel.ParcelState == true)
                    {
                        await PLService.DeliveryParcelByDrone(Drone.Id);
                        RefreshEvents.NotifyDroneChanged(drone.Id);
                        RefreshEvents.NotifyParcelChanged(drone.Parcel.Id);
                    }
                    else
                    {
                        await PLService.ParcelCollectionByDrone(Drone.Id);
                        RefreshEvents.NotifyDroneChanged(drone.Id);
                        RefreshEvents.NotifyParcelChanged(drone.Parcel.Id);
                    }
                }
                else
                {                   
                    await PLService.AssingParcelToDrone(Drone.Id);
                    RefreshEvents.NotifyDroneChanged(drone.Id);
                    RefreshEvents.NotifyParcelChanged();
                }
            } 
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show($"{ex.Id} {ex.Message}", $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message ,$"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.InvalidParcelStateException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NotExsistSuitibleStationException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Delete drone
        /// </summary>
        /// <param name="param"></param>
        public async void DeleteDrone(object param)
        {
            try
            {
                if (MessageBox.Show("You're sure you want to delete this drone?", "Delete Drone", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    await PLService.DeleteDrone(Drone.Id);
                    MessageBox.Show("The drone was successfully deleted");
                    RefreshEvents.DroneChangedEvent -= HandleADroneChanged;
                    RefreshEvents.NotifyDroneChanged(Drone.Id);
                    Tabs.CloseTab(param as TabItemFormat);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show($"{ex.Id} {ex.Message}", $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (BO.InvalidDroneStateException ex)
            {
                MessageBox.Show(ex.Message, $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.TheDroneIsNotInChargingException ex)
            {

                MessageBox.Show(ex.Message , $"Update Drone Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);

                if (simulatorWorker != null && !simulatorWorker.CancellationPending)
                    simulatorWorker.CancelAsync();
                if (simulatorWorker != null && !simulatorWorker.IsBusy)
                {
                    await PLService.DeleteDrone(Drone.Id);
                    MessageBox.Show("The drone was successfully deleted");
                    RefreshEvents.DroneChangedEvent -= HandleADroneChanged;
                    RefreshEvents.NotifyDroneChanged(drone.Id);
                    Tabs.CloseTab(param as TabItemFormat);
                }


            }
        }
        #region simulator
        private bool isAuto;

        /// <summary>
        /// Aotomatic
        /// </summary>
        public bool IsAuto
        {
            get { return isAuto; }
            set
            {
                Set(ref isAuto, value);
            }
        }

        /// <summary>
        /// Start simulator
        /// </summary>
        private void StartSimulator()
        {
            IsAuto = true;
            simulateDrone = StopSimulator;
            simulatorWorker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
            simulatorWorker.DoWork += (sender, args) => PLService.StartDroneSimulator(id, updateDrone, IsSimulatorStoped);
            simulatorWorker.RunWorkerCompleted += (sender, args) => IsAuto = false;
            simulatorWorker.ProgressChanged += HandleWorkerProgressChanged;
            simulatorWorker.RunWorkerAsync(id);
        }


        /// <summary>
        /// Handle worker progress changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void HandleWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RefreshEvents.NotifyDroneChanged(id);
            var ids = (ValueTuple<int?, int?, int?, int?>)e.UserState;
            if (ids.Item1 != null)
                RefreshEvents.NotifyParcelChanged(ids.Item1);
            else if (ids.Item2 != null)
                RefreshEvents.NotifyCustomerChanged(ids.Item2);
            else if (ids.Item3 != null)
                RefreshEvents.NotifyCustomerChanged(ids.Item3);
            else if (ids.Item4 != null)
                RefreshEvents.NotifyStationChanged(ids.Item4);
        }

        /// <summary>
        /// Stop  simulator
        /// </summary>
        private void StopSimulator()
        {
            simulateDrone = StartSimulator;
            simulatorWorker?.CancelAsync();
        }

        /// <summary>
        /// update drone
        /// </summary>
        /// <param name="parcelId">parcel's id</param>
        /// <param name="senderId">sender's id</param>
        /// <param name="receiverId">receiver's id</param>
        /// <param name="stationId">station's id</param>
        private void updateDrone(int? parcelId, int? senderId, int? receiverId, int? stationId)
        {
            simulatorWorker.ReportProgress(0, (parcelId, senderId, receiverId, stationId));
        }


        /// <summary>
        /// Is simulator stoped
        /// </summary>
        /// <returns>If simulator stoped</returns>
        private bool IsSimulatorStoped() => simulatorWorker.CancellationPending;

        #endregion

        /// <summary>
        /// Dispose the eventHandles
        /// </summary>
        public void Dispose()
        {
            RefreshEvents.DroneChangedEvent += HandleADroneChanged;
            simulatorWorker?.CancelAsync();
        }
    }
}
