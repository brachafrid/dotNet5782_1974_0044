using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLApi;
using DO;

namespace Dal
{
    public sealed partial class DalXml
    {
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            throw new NotImplementedException();
        }

        public void DeleteDrone(int id)
        {
            throw new NotImplementedException();
        }
        public Drone GetDrone(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> GetDrones()
        {
            throw new NotImplementedException();
        }

        public void RemoveDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public (double, double, double, double, double) GetElectricity()
        {
            throw new NotImplementedException();
        }
    }
}
