using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL:IblStationcs
    {

        public void AddStation(int id, string name, Location location, int chargeSlots)
        {
            if (ExistsIDTaxCheck(dal.GetStations(), id))
                throw new BO.AnElementWithTheSameKeyAlreadyExistsInTheListException();
            dal.addStation(id,name, location.Longitude, location.Longitude, chargeSlots);
            
        }
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            throw new NotImplementedException();

        }
        public IEnumerable<IDAL.DO.Station> GetStaionsWithEmptyChargeSlots()
        {
            throw new NotImplementedException();
        }
        public BO.Station GetStation(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw;
            return Map(dal.GetStation(id));
        }

        public IEnumerable<IDAL.DO.Station> GetStations()
        {
            throw new NotImplementedException();
        }
        private BO.Station Map(IDAL.DO.Station station)
        {
            return new Station() {
                Id = station.Id,
                Name = station.Name,
                Location = new Location() { Latitude=station.Latitude,Longitude=station.Longitude },
                AvailableChargingPorts=station.ChargeSlots-dal.countFullChargeSlots(station.Id),
            };
        }


    }
}
