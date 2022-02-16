using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    class UpdateStationVM : NotifyPropertyChangedBase,IDisposable
    {
        private readonly int id;
        /// <summary>
        /// Command of openning of drone charghing
        /// </summary>
        public RelayCommand OpenDroneChargeCommand { get; set; }

        private Station station;

        /// <summary>
        /// station
        /// </summary>
        public Station Station
        {
            get { return station; }
            set
            {
                Set(ref station, value);
            }
        }
        private string stationName;

        /// <summary>
        /// Station name
        /// </summary>
        public string StationName
        {
            get { return stationName; }
            set
            {
                Set(ref stationName, value);
             
            }
        }
        private int stationEmptyChargeSlots = 0;

        /// <summary>
        /// number empty charge slots in station
        /// </summary>
        public int StationEmptyChargeSlots
        {
            get { return stationEmptyChargeSlots; }
            set
            {
                Set(ref stationEmptyChargeSlots, value);
            }
        }

        /// <summary>
        /// Command of updating station
        /// </summary>
        public RelayCommand UpdateStationCommand { get; set; }
        /// <summary>
        /// Command of deleting station
        /// </summary>
        public RelayCommand DeleteStationCommand { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id of station</param>
        public UpdateStationVM(int id)
        {
            this.id = id;
            initStation();
            UpdateStationCommand = new(UpdateStation, param => Station?.Error == null);
            DeleteStationCommand = new(DeleteStation, param => Station?.Error == null);
            RefreshEvents.StationChangedEvent += HandleAStationChanged;
            OpenDroneChargeCommand = new(Tabs.OpenDetailes, null);
        }

        /// <summary>
        /// Handle station changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleAStationChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                initStation();
        }

        /// <summary>
        /// Initialize station
        /// </summary>
        public async void initStation()
        {
            try
            {
                Station = await PLService.GetStation(id);
                stationName = Station.Name;
                stationEmptyChargeSlots = Station.EmptyChargeSlots;
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Update station
        /// </summary>
        /// <param name="param"></param>
        public async void UpdateStation(object param)
        {
            try
            {
                if (stationName != Station.Name || stationEmptyChargeSlots != Station.EmptyChargeSlots)
                {
                    await PLService.UpdateStation(station.Id, station.Name, station.EmptyChargeSlots);
                    RefreshEvents.NotifyStationChanged(station.Id);
                    stationName = station.Name;
                    stationEmptyChargeSlots = station.EmptyChargeSlots;

                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Delete station
        /// </summary>
        /// <param name="param"></param>
        public async void DeleteStation(object param)
        {
            try
            {

                if (MessageBox.Show("Are You sure you want to delete this station?", "Delete Station", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                  await  PLService.DeleteStation(Station.Id);
                    MessageBox.Show("The station was successfully deleted");
                    RefreshEvents.StationChangedEvent -= HandleAStationChanged;
                    RefreshEvents.NotifyStationChanged(Station.Id);

                    Tabs.CloseTab(param as TabItemFormat);
                }
            }
            catch (BO.ThereAreAssociatedOrgansException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Update Station Id: {id}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Dispose the eventHandlers
        /// </summary>
        public void Dispose()
        {
            RefreshEvents.StationChangedEvent -= HandleAStationChanged;
        }
    }
}
