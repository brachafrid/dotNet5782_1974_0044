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
            DelegateVM.StationChangedEvent += (sender, e) => InitDrone();
            drone = new();
            AddDroneCommand = new(Add, param => drone.Error == null);
            Weight = Enum.GetValues(typeof(WeightCategories));
        }
        void InitDrone()
        {
            try
            {
                StationsId = PLService.GetStaionsWithEmptyChargeSlots().Select(station => station.Id);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
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
    }
}
