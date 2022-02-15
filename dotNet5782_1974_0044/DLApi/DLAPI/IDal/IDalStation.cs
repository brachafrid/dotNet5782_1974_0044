using System;
using System.Collections.Generic;
using DO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLApi
{
  public  interface IDalStation
    {
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public Station GetStation(int id);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty);
        public void UpdateStation(Station station, string name, int chargeSlots);
        public int CountFullChargeSlots(int id);
        public void DeleteStation(int id);
    }
}
