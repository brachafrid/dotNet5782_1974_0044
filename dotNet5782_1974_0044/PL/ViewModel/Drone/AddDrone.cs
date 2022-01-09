﻿using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    public class AddDroneVM
    {
        public DroneAdd drone { get; set; }
        public IEnumerable<int> StationsId { get; set; }
        public Array Weight { get; set; }
        public RelayCommand AddDroneCommand { get; set; }
        public AddDroneVM()
        {
            InitDrone();
            DelegateVM.Station += InitDrone;
            drone = new();
            AddDroneCommand = new(Add, param => drone.Error == null);
            Weight = Enum.GetValues(typeof(WeightCategories));
        }
        void InitDrone()
        {
            StationsId = PLService.GetStaionsWithEmptyChargeSlots().Select(station => station.Id);
        }
        public void Add(object param)
        {
            try
            {
                PLService.AddDrone(drone);
                DelegateVM.Drone?.Invoke();                             
                DelegateVM.Station?.Invoke();
                Tabs.CloseTab((param as TabItemFormat).Header);

            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("id has already exist");
                drone.Id = null;
            }

        }
    }
}
