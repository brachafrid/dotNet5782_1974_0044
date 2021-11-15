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

        public void AddStation(Station stationBL)
        {
            if (ExistsIDTaxCheck(dal.GetStations(), stationBL.Id))
                throw new AnElementWithTheSameKeyAlreadyExistsInTheListException();
            dal.addStation(stationBL.Id, stationBL.Name, stationBL.Location.Longitude, stationBL.Location.Longitude, stationBL.AvailableChargingPorts);
            
        }
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Station satationDl = dal.GetStation(id);
            if (name.Equals(default(string)) && chargeSlots.Equals(default(int)))
                throw new ArgumentNullException("For updating at least one parameter must be initialized ");
            if (!chargeSlots.Equals(default(int)) && chargeSlots < dal.countFullChargeSlots(satationDl.Id))
                throw new ArgumentOutOfRangeException("The number of charging slots is smaller than the number of slots used");
            dal.RemoveStation(satationDl);
            dal.addStation(id, name.Equals(default(string)) ? satationDl.Name : name, satationDl.Longitude, satationDl.Latitude, chargeSlots.Equals(default(int)) ? satationDl.ChargeSlots : chargeSlots);
        }
        public IEnumerable<Station> GetStaionsWithEmptyChargeSlots()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetSationsWithEmptyChargeSlots();
            List<Station> stations = new List<Station>();
            foreach (var item in list)
            {
                stations.Add(MapStation(item));
            }
            return stations;
        }
        public Station GetStation(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw new KeyNotFoundException();
            return MapStation(dal.GetStation(id));
        }

        public IEnumerable<Station> GetStations()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetStations();
            List<Station> stations = new List<Station>();
            foreach (var item in list)
            {
                stations.Add(MapStation(item));
            }
            return stations;
        }
        private BO.Station MapStation(IDAL.DO.Station station)
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
