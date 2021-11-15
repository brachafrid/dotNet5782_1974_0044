﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public interface IblStationcs
    {
        public void AddStation(BO.Station station);
        public void UpdateStation(int id, string name, int chargeSlots);
        public Station GetStation(int id);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Station> GetStaionsWithEmptyChargeSlots();
    }
}
