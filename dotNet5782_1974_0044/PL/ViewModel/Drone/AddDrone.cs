using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class AddDroneVM
    {
        public DroneAdd drone { get; set; }
        public List<int> StationsId { get; set; }
        public Array Weight { get; set; }
        public RelayCommand AddDroneCommand { get; set; }
        public AddDroneVM()
        {
            update();
            DelegateVM.Station += update;
            drone = new();
            AddDroneCommand = new(Add, param => drone.Error == null);
            Weight = Enum.GetValues(typeof(WeightCategories));
        }
        void update()
        {
            StationsId = new StationHandler().GetStaionsWithEmptyChargeSlots().Select(station => station.Id).ToList();
        }
        public void Add(object param)
        {
            try
            {
                new DroneHandler().AddDrone(drone);
                MessageBox.Show("success");
                DelegateVM.Drone?.Invoke();             
                Tabs.TabItems.RemoveAt(Tabs.TabItems.IndexOf(Tabs.TabItems.FirstOrDefault(tab => tab.Text == "Drone")));
                DelegateVM.Station?.Invoke();

            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("id has already exist");
                drone.Id = null;
            }

        }
    }
}
