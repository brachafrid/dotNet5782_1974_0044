using BO;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// Calculate the electricity a droneneed to bring a parcel to the desatination
        /// </summary>
        /// <param name="aviableDrone">The drone that take the parcel</param>
        /// <param name="CustomerSender">The the current locatio of the parcel</param>
        /// <param name="CustomerReceives">The the destination locatio of the parcel</param>
        /// <param name="weight">The weight of the parcels</param>
        /// <param name="distance">The distance the drone traveling</param>
        /// <returns></returns>
        private double CalculateElectricity(Location aviableDroneLocation, double? batteryStatus, Location CustomerSender, Location CustomerReceives, WeightCategories weight, out double distance)
        {
            double electricity;
            double e = weight switch
            {
                WeightCategories.LIGHT => lightWeightCarrier,
                WeightCategories.MEDIUM => mediumWeightBearing,
                WeightCategories.HEAVY => carriesHeavyWeight,
               _ =>0
            };
            Station station;
            electricity = Distance(aviableDroneLocation, CustomerSender) * available +
                        Distance(CustomerSender, CustomerReceives) * e;
            try
            {
                station = batteryStatus != null ? ClosetStationPossible(aviableDroneLocation, (int chargeSlots) => chargeSlots > 0,(double)batteryStatus - electricity, out _) : ClosetStation(aviableDroneLocation, (int chargeSlots) => chargeSlots > 0);
                if (station == null)
                {
                    distance = 0;
                    return 101d;
                }

                electricity += Distance(CustomerReceives,
                             station.Location) * available;
                distance = Distance(aviableDroneLocation, CustomerSender) +
                    Distance(CustomerSender, CustomerReceives) +
                    Distance(CustomerReceives, station.Location);
                return electricity;
            }
            catch (NotExsistSuitibleStationException ex)
            {
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException(ex.Message, ex);
            }
        }
    }
}

