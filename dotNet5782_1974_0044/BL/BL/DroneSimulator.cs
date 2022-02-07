using BO;
using DLApi;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BL
{
    internal class DroneSimulator
    {
        enum Maintenance { Starting, Going, Charging }
        enum Delivery { Starting, Going, Delivery }
        BL bl { set; get; }
        Parcel parcel { set; get; }
        Station Station { set; get; }
        IDal Dal { set; get; }
        Drone Drone { set; get; }
        private const int DELAY = 500;
        private Maintenance maintenance = Maintenance.Starting;
        private Delivery delivery = Delivery.Going;
        private const double TIME_STEP = DELAY / 1000.0;
        private const double VELOCITY = 1.0;
        private const double STEP = VELOCITY / TIME_STEP;
        double distance = 0.0;
        public DroneSimulator(int id, BL BL, Action update)
        {
            try
            {
                bl = BL;
                Dal = bl.dal;
                Drone = bl.GetDrone(id);
                while(true)
                {
                    
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("", ex);
            }
        }
        private void AvailbleDrone()
        {
            lock (bl)
            {
                try
                {
                    bl.AssingParcelToDrone(Drone.Id);
                }
                catch (NotExsistSutibleParcelException)
                {
                    if (Drone.BattaryMode == 100)
                        return;
                    Drone.DroneState = DroneState.MAINTENANCE;
                    maintenance = Maintenance.Starting;
                }
            }
        }
        private void MaintenanceDrone()
        {
            switch (maintenance)
            {
                case Maintenance.Starting:
                    {
                        lock (bl)
                        {
                            try
                            {
                                Station = bl.ClosetStationPossible(Drone.CurrentLocation, Drone.BattaryMode, out double minDistance);
                                maintenance = Maintenance.Going;
                                distance = BL.Distance(Drone.CurrentLocation, Station.Location);
                            }
                            catch (NotExsistSuitibleStationException ex)
                            {

                            }
                        }

                        break;
                    }

                case Maintenance.Going:
                    {
                        if (distance < 0.1)
                            maintenance = Maintenance.Charging;
                        else
                            Drone.CurrentLocation = UpdateLocation(Station.Location, bl.available);
                        break;
                    }

                case Maintenance.Charging:
                    {
                        if (Drone.BattaryMode == 100)
                            bl.ReleaseDroneFromCharging(Drone.Id);
                        else
                            lock (bl) Drone.BattaryMode = Math.Min(1.0, Drone.BattaryMode + bl.droneLoadingRate * TIME_STEP);
                        break;
                    }
                default:
                    break;
            }
        }
        private void DeliveryDrone()
        {
            try
            {
                lock (bl)
                {
                    parcel = bl.GetParcel(Drone.Parcel.Id);
                }
                switch (delivery)
                {
                    case Delivery.Starting:
                    {
                        lock(bl)
                        {
                            distance = BL.Distance(Drone.CurrentLocation, bl.GetCustomer(parcel.CustomerSender.Id).Location);
                        }
                            break;
                    }
                    case Delivery.Going:
                        {
                            lock (bl)
                            {
                                
                                Drone.CurrentLocation = UpdateLocation(bl.GetCustomer(parcel.CustomerSender.Id).Location, bl.available);
                            }
                            break;
                        }
                    case Delivery.Delivery:
                        {

                            break;
                        }

                    default:
                        break;
                }

            }
            catch (KeyNotFoundException ex)
            {
                Drone.DroneState = DroneState.AVAILABLE;
            }



        }
        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
        private Location UpdateLocation(Location Target, double elec)
        {
            double delta = distance < STEP ? distance : STEP;
            double proportion = delta / distance;
            Drone.BattaryMode = Math.Max(0.0, Drone.BattaryMode - delta * elec);
            double lat = Drone.CurrentLocation.Latitude + (Target.Latitude - Drone.CurrentLocation.Latitude) * proportion;
            double lon = Drone.CurrentLocation.Longitude + (Target.Longitude - Drone.CurrentLocation.Longitude) * proportion;
            return new() { Latitude = lat, Longitude = lon };
        }

    }
}
