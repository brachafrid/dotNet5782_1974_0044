﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BL
{
    internal class DroneSimulator
    {
        enum Maintenance { Starting, Going, Charging }
        enum Delivery { Starting, Going, Delivery }
        int? parcelId;
        int? senderId;
        int? reciverId;
        int? stationId;
        BL bl { set; get; }
        Parcel parcel { set; get; }
        Station Station { set; get; }
        DroneToList Drone { set; get; }
        private const int DELAY = 500;
        private Maintenance maintenance = Maintenance.Starting;
        private Delivery delivery = Delivery.Starting;
        private const double TIME = 0.5;
        private const double STEP = 1500;
        double distance = 0.0;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">drone id</param>
        /// <param name="BL">BL</param>
        /// <param name="update">Action - update</param>
        /// <param name="IsCheckStop">Func - check stop</param>
        public DroneSimulator(int id, BL BL, Action<int?, int?, int?, int?> update, Func<bool> IsCheckStop)
        {
            try
            {
                bl = BL;
                lock (bl)
                {
                    Drone = bl.drones.FirstOrDefault(Drone => Drone.Id == id);
                    if (Drone.DroneState == DroneState.DELIVERY && bl.GetParcel((int)Drone.ParcelId).CollectionTime != null)
                        delivery = Delivery.Delivery;
                }
                while (!IsCheckStop())
                {
                    parcelId = senderId = reciverId = stationId = null;
                    if (IsSleepDelayTime())
                        switch (Drone.DroneState)
                        {
                            case DroneState.AVAILABLE:
                                AvailbleDrone();
                                break;
                            case DroneState.MAINTENANCE:
                                MaintenanceDrone();
                                break;
                            case DroneState.DELIVERY:
                                DeliveryDrone();
                                break;
                            default:
                                break;
                        }
                    update(parcelId, senderId, reciverId, stationId);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("", ex);
            }
            catch (NotExsistSuitibleStationException)
            {
                lock (bl)
                    Drone.DroneState = DroneState.RESCUE;
            }
        }
        /// <summary>
        /// Availble drone
        /// </summary>
        private void AvailbleDrone()
        {
            lock (bl)
            {
                try
                {
                    bl.AssingParcelToDrone(Drone.Id);
                    parcelId = Drone.ParcelId;
                }
                catch (NotExsistSutibleParcelException)
                {
                    if (Drone.BatteryState >= 100)
                        return;
                    Drone.DroneState = DroneState.MAINTENANCE;
                    maintenance = Maintenance.Starting;
                }
            }
        }
        /// <summary>
        /// Maintenance drone
        /// </summary>
        private void MaintenanceDrone()
        {

            switch (maintenance)
            {
                case Maintenance.Starting:
                    {
                        lock (bl)
                        {
                            Station = bl.ClosetStationPossible(Drone.CurrentLocation, (int chargeSlots) => chargeSlots > 0, Drone.BatteryState, out double n);
                            if (Station == null)
                            {
                                Drone.DroneState = DroneState.RESCUE;
                                break;
                            }
                            distance = BL.Distance(Drone.CurrentLocation, Station.Location);
                            maintenance = Maintenance.Going;

                        }
                        break;
                    }
                case Maintenance.Going:
                    {
                        if (distance < 0.001)
                            lock (bl)
                            {
                                Drone.DroneState = DroneState.AVAILABLE;
                                bl.SendDroneForCharg(Drone.Id);
                                maintenance = Maintenance.Charging;
                            }
                        else
                        {
                            lock (bl)
                            {
                                Drone.CurrentLocation = UpdateLocationAndBattary(Station.Location, bl.available);
                                distance = BL.Distance(Drone.CurrentLocation, Station.Location);
                            }
                        }
                        break;
                    }
                case Maintenance.Charging:
                    {
                        lock (bl)
                            if (Station == null)
                                Station = bl.GetAllStations().Select(station => bl.GetStation(station.Id)).FirstOrDefault(station => station.DroneInChargings.FirstOrDefault(drone => drone.Id == Drone.Id) != null);
                        lock (bl)
                            if (Drone.BatteryState == 100)
                            {

                                bl.ReleaseDroneFromCharging(Drone.Id);
                                delivery = Delivery.Starting;
                            }
                            else
                                lock (bl) Drone.BatteryState = Math.Min(100, Drone.BatteryState + bl.droneLoadingRate * TIME);
                        stationId = Station.Id;
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Delivery drone
        /// </summary>
        private void DeliveryDrone()
        {
            try
            {
                if (parcel == null)
                    lock (bl)
                        parcel = bl.GetParcel((int)Drone.ParcelId);
                switch (delivery)
                {
                    case Delivery.Starting:
                        {
                            lock (bl)
                                distance = BL.Distance(Drone.CurrentLocation, bl.GetCustomer(parcel.CustomerSender.Id).Location);
                            delivery = Delivery.Going;
                            break;
                        }
                    case Delivery.Going:
                        {
                            if (distance > 0.01)
                            {
                                lock (bl)
                                {
                                    Drone.CurrentLocation = UpdateLocationAndBattary(bl.GetCustomer(parcel.CustomerSender.Id).Location, bl.available);
                                    distance = BL.Distance(Drone.CurrentLocation, bl.GetCustomer(parcel.CustomerSender.Id).Location);
                                }
                            }

                            else
                            {
                                lock (bl)
                                {
                                    try
                                    {
                                        delivery = Delivery.Delivery;
                                        Drone.CurrentLocation = bl.GetCustomer(parcel.CustomerSender.Id).Location;
                                        distance = BL.Distance(Drone.CurrentLocation, bl.GetCustomer(parcel.CustomerReceives.Id).Location);
                                        bl.ParcelCollectionByDrone(Drone.Id);
                                        parcelId = Drone.ParcelId;
                                        delivery = Delivery.Delivery;
                                    }
                                    catch (ArgumentNullException)
                                    {
                                        delivery = Delivery.Delivery;
                                    }
                                }
                            }

                            break;
                        }

                    case Delivery.Delivery:
                        {
                            if (distance < 0.001)
                            {
                                lock (bl)
                                {
                                    bl.DeliveryParcelByDrone(Drone.Id);
                                    Drone.DroneState = DroneState.AVAILABLE;
                                    parcel = null;
                                    parcelId = Drone.ParcelId;
                                }
                            }
                            else
                            {
                                lock (bl)
                                {
                                    double elect = bl.GetParcel((int)Drone.ParcelId).Weight switch
                                    {
                                        WeightCategories.HEAVY => bl.carriesHeavyWeight,
                                        WeightCategories.MEDIUM => bl.mediumWeightBearing,
                                        WeightCategories.LIGHT => bl.lightWeightCarrier,
                                        _ => 0.0
                                    };
                                    Drone.CurrentLocation = UpdateLocationAndBattary(bl.GetCustomer(parcel.CustomerReceives.Id).Location, elect);
                                    distance = BL.Distance(Drone.CurrentLocation, bl.GetCustomer(parcel.CustomerReceives.Id).Location);
                                }
                            }

                            break;
                        }

                    default:
                        break;
                }

            }
            catch (KeyNotFoundException)
            {
                lock (bl)
                    Drone.DroneState = DroneState.AVAILABLE;
            }
        }

        /// <summary>
        /// Sleep delay time
        /// </summary>
        /// <returns>if succeeded</returns>
        private static bool IsSleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }

        /// <summary>
        /// Update location and battary
        /// </summary>
        /// <param name="Target">location of target</param>
        /// <param name="elec">electrecity</param>
        /// <returns>location</returns>
        private Location UpdateLocationAndBattary(Location Target, double elec)
        {
            double delta = distance < STEP ? distance : STEP;
            double proportion = delta / distance;
            double lat, lon;
            lock (bl)
            {
                Drone.BatteryState = Math.Max(0.0, Drone.BatteryState - delta * elec);
                lat = Drone.CurrentLocation.Latitude + (Target.Latitude - Drone.CurrentLocation.Latitude) * proportion;
                lon = Drone.CurrentLocation.Longitude + (Target.Longitude - Drone.CurrentLocation.Longitude) * proportion;
            }
            return new() { Latitude = lat, Longitude = lon };
        }

    }
}
