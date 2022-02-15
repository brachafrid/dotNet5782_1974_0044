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
        public DroneAdd drone { get; set; }
        public ObservableCollection<int> StationsId { get; set; }
        public Array Weight { get; set; }
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

        public void Dispose()
        {
            DelegateVM.StationChangedEvent -= HandleStationListChanged;
        }
    }
}
