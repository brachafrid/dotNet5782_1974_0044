﻿using BO;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    class UpdateStationVM : DependencyObject
    {
        private int id;
        public RelayCommand OpenDroneChargeCommand { get; set; }
        public PO.Station station
        {
            get { return (PO.Station)GetValue(stationProperty); }
            set { SetValue(stationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationProperty =
            DependencyProperty.Register("station", typeof(PO.Station), typeof(UpdateStationVM), new PropertyMetadata(new PO.Station()));



        public string stationName
        {
            get { return (string)GetValue(stationNameProperty); }
            set { SetValue(stationNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationNameProperty =
            DependencyProperty.Register("stationName", typeof(string), typeof(UpdateStationVM), new PropertyMetadata(""));



        public int stationEmptyChargeSlots
        {
            get { return (int)GetValue(stationEmptyChargeSlotsProperty); }
            set { SetValue(stationEmptyChargeSlotsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customerPhone.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationEmptyChargeSlotsProperty =
            DependencyProperty.Register("stationEmptyChargeSlots", typeof(int), typeof(UpdateStationVM), new PropertyMetadata(0));


        public RelayCommand UpdateStationCommand { get; set; }
        public RelayCommand DeleteStationCommand { get; set; }

        public UpdateStationVM(int id)
        {
            this.id = id;
            init();
            stationName = station.Name;
            stationEmptyChargeSlots = station.EmptyChargeSlots;
            UpdateStationCommand = new(UpdateStation, param => station.Error == null);
            DeleteStationCommand = new(DeleteStation, param => station.Error == null);
            DelegateVM.Station += init;
            OpenDroneChargeCommand = new(OpenDroneDetails, null);
        }
        public void init()
        {
            station = PLService.GetStation(id);
        }

        public void UpdateStation(object param)
        {
            try
            {
                if (stationName != station.Name || stationEmptyChargeSlots != station.EmptyChargeSlots)
                {
                    PLService.UpdateStation(station.Id, station.Name, station.EmptyChargeSlots);
                    DelegateVM.Station();
                    stationName = station.Name;
                    stationEmptyChargeSlots = station.EmptyChargeSlots;
                
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
        public void DeleteStation(object param)
        {
            try { 

                if (MessageBox.Show("You're sure you want to delete this station?", "Delete Station", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    PLService.DeleteStation(station.Id);
                    MessageBox.Show("The station was successfully deleted");
                }
            }
              catch (ThereAreAssociatedOrgansException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public void OpenDroneDetails(object param)
        {
            if (param != null && param is PL.PO.DroneInCharging droneCharge)
                Tabs.AddTab(new()
                {
                    TabContent = "UpdateDroneView",
                    Text = "drone " + droneCharge.Id,
                    Id = droneCharge.Id
                });
        }

    }
}
