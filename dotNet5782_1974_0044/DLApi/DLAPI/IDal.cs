﻿using System;
using System.Collections.Generic;
using DO;

namespace DLApi
{
    public interface IDal
    {
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude);
        public void AddDrone(int id, string model, WeightCategories MaxWeight);
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = default, DateTime? sceduled = default, DateTime? pickedUp = default, DateTime? delivered = default);
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void RemoveCustomer(Customer customer);
        public Station GetStation(int id);
        public Drone GetDrone(int id);
        public Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign);
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty);
        public void RemoveStation(Station station);
        public int CountFullChargeSlots(int id);
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation);
        public void RemoveDrone(Drone drone);
        public void AddDRoneCharge(int droneId, int stationId);
        public void RemoveDroneCharge(int droneId);
        public DateTime GetTimeStartOfCharge(int droneId);
        (double, double, double, double, double) GetElectricity();
        public void RemoveParcel(Parcel parcel);
        public void DeleteCustomer(int id);
        public void DeleteDrone(int id);
        public void DeleteParcel(int id);
        public void DeleteStation(int id);
        public string GetAdministorPasssword();
    }
}
