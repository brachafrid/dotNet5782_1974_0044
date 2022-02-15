using DO;
using System.Collections.Generic;

namespace DLApi
{
    public interface IDalDrone
    {
        public void AddDrone(int id, string model, WeightCategories MaxWeight);
        public Drone GetDrone(int id);
        public IEnumerable<Drone> GetDrones();
        public void UpdateDrone(Drone drone,string model);
        public void DeleteDrone(int id);
    }
}
