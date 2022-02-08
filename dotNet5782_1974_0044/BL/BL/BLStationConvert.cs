using BO;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// Convert a DAL station to BLStationToList satation
        /// </summary>
        /// <param name="station">The sation to convert</param>
        /// <returns>The converted station</returns>
        private BO.StationToList MapStationToList(DO.Station station)
        {
            lock (dal)
                return new StationToList()
                {
                    Id = station.Id,
                    Name = station.Name,
                    EmptyChargeSlots = station.ChargeSlots - dal.CountFullChargeSlots(station.Id),
                    FullChargeSlots = dal.CountFullChargeSlots(station.Id)
                };
        }

        /// <summary>
        /// Convert a DAL station to BL satation
        /// </summary>
        /// <param name="station">The sation to convert</param>
        /// <returns>The converted station</returns>
        private BO.Station ConvertStation(DO.Station station)
        {
            lock (dal)
                return new Station()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Location = new Location() { Latitude = station.Latitude, Longitude = station.Longitude },
                    AvailableChargingPorts = station.ChargeSlots - dal.CountFullChargeSlots(station.Id),
                    DroneInChargings = CreatListDroneInCharging(station.Id)
                };
        }
    }
}
