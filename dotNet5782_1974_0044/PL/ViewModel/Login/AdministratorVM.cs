﻿using PL.PO;
using System.Collections.ObjectModel;
using System.Linq;

namespace PL
{
    public  class AdministratorVM
    {
        public  RelayCommand AddDroneToListWindowCommand { get; set; }
        public  RelayCommand AddParcelToListWindowCommand { get; set; }
        public  RelayCommand AddStationToListWindowCommand { get; set; }
        public  RelayCommand AddCustomerToListWindowCommand { get; set; }


        public AdministratorVM()
        {
            AddDroneToListWindowCommand = new(AddDroneToList, null);
            AddParcelToListWindowCommand = new(AddParcelToList, null);
            AddStationToListWindowCommand = new(AddStationToList, null);
            AddCustomerToListWindowCommand = new(AddCustomeroList, null);
        }

        public  void AddDroneToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Drones",
                TabContent = "DroneToListWindow"
            });
            
        }
        public  void AddParcelToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Parcels",
                TabContent = "ParcelToListWindow"
            });
            
        }
        public  void AddStationToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Stations",
                TabContent = "StationToListWindow"
            });
           
        }
        public  void AddCustomeroList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Customers",
                TabContent = "CustomerTolistWindow"
            });
           
        }
    }
}
