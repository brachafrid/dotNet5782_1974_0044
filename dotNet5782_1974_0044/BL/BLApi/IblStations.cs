
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
        public IEnumerable<StationToList> GetAllStations();
        public IEnumerable<StationToList> GetActiveStations();
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots(Predicate<int> exsitEmpty);
        public void DeleteStation(int id);
        public bool IsNotActiveStation(int id);

    }
}

