
using System.Collections.Generic;
using System;
using BO;

namespace BLApi
{
    public interface IBlStations
    {
        public void AddStation(BO.Station station);
        public void UpdateStation(int id, string name, int chargeSlots);
        public Station GetStation(int id);
        public IEnumerable<StationToList> GetStations();
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots(Predicate<int> exsitEmpty);
    }
}

