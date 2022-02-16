using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL
{
    public class AddDroneVM : NotifyPropertyChangedBase, IDisposable
    {
        /// <summary>
        /// The added drone
        /// </summary>
        public DroneAdd drone { get; set; }

        private ObservableCollection<int> stationsId;

        /// <summary>
        /// ObservableCollection of stations keys
        /// </summary>
        public ObservableCollection<int> StationsId
        {
            get => stationsId;
            set => Set(ref stationsId, value);
        }

        /// <summary>
        /// Array of weights
        /// </summary>
        public Array Weight { get; set; }
        /// <summary>
        /// Command of adding drone
        /// </summary>
        public RelayCommand AddDroneCommand { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public AddDroneVM()
        {
            InitDrone();
            RefreshEvents.StationChangedEvent += HandleStationListChanged;
            drone = new();
            AddDroneCommand = new(Add, param => drone.Error == null);
            Weight = Enum.GetValues(typeof(WeightCategories));
        }

        /// <summary>
        /// Initialize drone
        /// </summary>
        async void InitDrone()
        {
            try
            {
                StationsId = new ObservableCollection<int>((await PLService.GetStaionsWithEmptyChargeSlots()).Select(station => station.Id));
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Adding Drone", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles changes to the list of stations
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private async void HandleStationListChanged(object sender, EntityChangedEventArgs e)
        {
            try
            {
                StationsId.Clear();
                foreach (var item in (await PLService.GetStaionsWithEmptyChargeSlots()).Select(station => station.Id))
                    StationsId.Add(item);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Adding Drone", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Add a drone
        /// </summary>
        /// <param name="param"></param>
        public async void Add(object param)
        {
            try
            {
                await PLService.AddDrone(drone);
                RefreshEvents.NotifyDroneChanged(drone.Id ?? 0);
                RefreshEvents.NotifyStationChanged(drone.StationId);
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show( ex.Message+Environment.NewLine+$"The Id: {ex.Id}", "Adding Drone", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                drone.Id = null;
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Adding Drone", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Adding Drone", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Dispose the eventHandles
        /// </summary>
        public void Dispose()
        {
            RefreshEvents.StationChangedEvent -= HandleStationListChanged;
        }
    }
}
