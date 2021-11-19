using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

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
        private double calculateElectricity(DroneToList aviableDrone, Location CustomerSender, Location CustomerReceives, WeightCategories weight, out double distance)
        {
            DroneToList tempDrone = aviableDrone;
            double electricity;
            IDAL.DO.Station station;
            electricity = Distance(aviableDrone.CurrentLocation, CustomerSender) * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE] +
                        Distance(CustomerSender, CustomerReceives) * dal.GetElectricityUse()[(int)weight + 1];
            tempDrone.BatteryStatus -= electricity;
            station = ClosetStationPossible(dal.GetStations(), tempDrone, out distance);
            electricity += Distance(CustomerReceives,
                         new Location() { Latitude = station.Latitude, Longitude = station.Longitude }) * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            distance = Distance(aviableDrone.CurrentLocation, CustomerSender) +
                Distance(CustomerSender, CustomerReceives) +
                Distance(CustomerReceives, new Location() { Latitude = station.Latitude, Longitude = station.Longitude });
            return electricity;
        }
    }
}
