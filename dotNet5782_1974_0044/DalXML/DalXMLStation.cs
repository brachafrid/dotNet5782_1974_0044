using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    public sealed partial class DalXml
    {
        public void RemoveStation(Station station)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStations()
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }

        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            throw new NotImplementedException();
        }
    }
}
