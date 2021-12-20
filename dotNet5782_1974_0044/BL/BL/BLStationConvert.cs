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
            return new StationToList()
            {
                Id = station.Id,
                Name = station.Name,
                EmptyChargeSlots = station.ChargeSlots - dal.CountFullChargeSlots(station.Id),
                FullChargeSlots = dal.CountFullChargeSlots(station.Id)
            };
        }


    }
}
