using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
                DelegateVM.NotifyDroneChanged(drone.Id ?? 0);
                DelegateVM.NotifyStationChanged(drone.StationId);
                Tabs.CloseTab(param as TabItemFormat);

            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("id has already exist");
                drone.Id = null;
            }

        }
    }
}
