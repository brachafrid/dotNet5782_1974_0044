﻿using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL
{
    public class AddDroneVM : IDisposable
    {
        /// <summary>
        /// The added drone
        /// </summary>
        public DroneAdd drone { get; set; }
        /// <summary>
        /// ObservableCollection of stations keys
        /// </summary>
        public ObservableCollection<int> StationsId { get; set; }
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
            DelegateVM.StationChangedEvent += HandleStationListChanged;
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
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }

        /// <summary>
        /// Handles changes to the list of stations
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private async void HandleStationListChanged(object sender, EntityChangedEventArgs e)
        {
            StationsId.Clear();
            foreach (var item in (await PLService.GetStaionsWithEmptyChargeSlots()).Select(station => station.Id))
                StationsId.Add(item);
        }

        /// <summary>
        /// Add a drone
        /// </summary>
        /// <param name="param"></param>
        public async void Add(object param)
        {
            try
            {
                Tabs.CloseTab(param as TabItemFormat);
                await PLService.AddDrone(drone);
                DelegateVM.NotifyDroneChanged(drone.Id ?? 0);
                DelegateVM.NotifyStationChanged(drone.StationId);
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("id has already exist");
                drone.Id = null;
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

        /// <summary>
        /// Dispose the eventHandles
        /// </summary>
        public void Dispose()
        {
            DelegateVM.StationChangedEvent -= HandleStationListChanged;
        }
    }
}
