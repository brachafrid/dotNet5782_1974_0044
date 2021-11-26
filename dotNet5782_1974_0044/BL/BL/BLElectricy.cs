﻿using IBL.BO;

namespace IBL
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
        private double CalculateElectricity(Location aviableDroneLocation,double? batteryStatus, Location CustomerSender, Location CustomerReceives, WeightCategories weight, out double distance)
        {
            double electricity;
            double e= weight switch
            {
                WeightCategories.LIGHT => lightWeightCarrier,
                WeightCategories.MEDIUM => mediumWeightBearing,
                WeightCategories.HEAVY => carriesHeavyWeight
            };
            IDAL.DO.Station station;
            electricity = Distance(aviableDroneLocation, CustomerSender) * available +
                        Distance(CustomerSender, CustomerReceives) * e;
            station =batteryStatus!=null? ClosetStationPossible(dal.GetStations(), aviableDroneLocation,(double)batteryStatus-electricity, out _ ):ClosetStation(dal.GetStations(),aviableDroneLocation);
            electricity += Distance(CustomerReceives,
                         new Location() { Latitude = station.Latitude, Longitude = station.Longitude }) * available;
            distance = Distance(aviableDroneLocation, CustomerSender) +
                Distance(CustomerSender, CustomerReceives) +
                Distance(CustomerReceives, new Location() { Latitude = station.Latitude, Longitude = station.Longitude });
            return electricity;
        }

    }
}

