using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class AddDroneView
    {
        public DroneAdd drone { get; set; } = new();
        public List<int> StationsId { get; set; }
        public Array Weight = Enum.GetValues(typeof(PO.WeightCategories));
        public RelayCommand AddDroneCommand { get; set; }
        public AddDroneView()
        {
            StationsId = new StationHundler().GetStaionsWithEmptyChargeSlots().Select(station => station.Id).ToList();
            AddDroneCommand = new(Add, param => drone.Error == null);
        }
        public void Add(object param)
        {
            new DroneHandler().AddDrone(drone);
        }
    }
}
