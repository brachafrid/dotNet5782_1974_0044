using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class AddDroneVM
    {
        public DroneAdd drone { get; set; } = new();
        public List<int> StationsId { get; set; }
        public Array Weight { get; set; }
        public RelayCommand AddDroneCommand { get; set; }
        public AddDroneVM()
        {
            StationsId = new StationHundler().GetStaionsWithEmptyChargeSlots().Select(station => station.Id).ToList();
            AddDroneCommand = new(Add, param => drone.Error == null);
            Weight = Enum.GetValues(typeof(WeightCategories));
        }
        public void Add(object param)
        {
            new DroneHandler().AddDrone(drone);
        }
    }
}
