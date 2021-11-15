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
                throw new AnElementWithTheSameKeyAlreadyExistsInTheListException();
            dal.addStation(id,name, location.Longitude, location.Longitude, chargeSlots);
            
        }
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Station station = dal.GetStation(id);
            if (name.Equals(default(string)) && chargeSlots.Equals(default(int)))
                throw new ArgumentNullException("For updating at least one parameter must be initialized ");
            if (!chargeSlots.Equals(default(int)) && chargeSlots < dal.countFullChargeSlots(station.Id))
                throw new ArgumentOutOfRangeException("The number of charging slots is smaller than the number of slots used");
            dal.RemoveStation(station);
            dal.addStation(id, name.Equals(default(string)) ? station.Name : name, station.Longitude, station.Latitude, chargeSlots.Equals(default(int)) ? station.ChargeSlots : chargeSlots);
        }
        public IEnumerable<Station> GetStaionsWithEmptyChargeSlots()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetSationsWithEmptyChargeSlots();
            List<Station> stations = new List<Station>();
            foreach (var item in list)
            {
                stations.Add(Map(item));
            }
            return stations;
        }
        public Station GetStation(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw new KeyNotFoundException();
            return Map(dal.GetStation(id));
        }

        public IEnumerable<Station> GetStations()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetStations();
            List<Station> stations = new List<Station>();
            foreach (var item in list)
            {
                stations.Add(Map(item));
            }
            return stations;
        }
        private BO.Station Map(IDAL.DO.Station station)
        {
            return new Station() {
                Id = station.Id,
                Name = station.Name,
                Location = new Location() { Latitude=station.Latitude,Longitude=station.Longitude },
                AvailableChargingPorts=station.ChargeSlots-dal.countFullChargeSlots(station.Id),
                DroneInChargings=CreatList(station.Id)
            };
        }

    }
}
