using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    class UpdateStationVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }

        private readonly int id;
        public RelayCommand OpenDroneChargeCommand { get; set; }
        private Station station;

        public Station Station
        {
            get { return station; }
            set
            {
                station = value;
                onPropertyChanged("Station");
            }
        }
        private string stationName;

        public string StationName
        {
            get { return stationName; }
            set
            {
                stationName = value;
                onPropertyChanged("StationName");
            }
        }
        private int stationEmptyChargeSlots = 0;

        public int StationEmptyChargeSlots
        {
            get { return stationEmptyChargeSlots; }
            set
            {
                stationEmptyChargeSlots = value;
                onPropertyChanged("StationEmptyChargeSlots");
            }
        }

        public RelayCommand UpdateStationCommand { get; set; }
        public RelayCommand DeleteStationCommand { get; set; }

        public UpdateStationVM(int id)
        {
            this.id = id;
            initStation();
            stationName = Station.Name;
            stationEmptyChargeSlots = Station.EmptyChargeSlots;
            UpdateStationCommand = new(UpdateStation, param => Station.Error == null);
            DeleteStationCommand = new(DeleteStation, param => Station.Error == null);
            DelegateVM.StationChangedEvent += HandleAStationChanged;
            OpenDroneChargeCommand = new(Tabs.OpenDetailes, null);
        }
        private void HandleAStationChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                initStation();
        }
        public void initStation()
        {
            try
            {
                station = PLService.GetStation(id);
            }
            catch (KeyNotFoundException)
            {

            }

        }

        public async void UpdateStation(object param)
        {
            try
            {
                if (stationName != Station.Name || stationEmptyChargeSlots != Station.EmptyChargeSlots)
                {
                    await PLService.UpdateStation(station.Id, station.Name, station.EmptyChargeSlots);
                    DelegateVM.NotifyStationChanged(station.Id);
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
            try
            {

                if (MessageBox.Show("You're sure you want to delete this station?", "Delete Station", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    PLService.DeleteStation(Station.Id);
                    MessageBox.Show("The station was successfully deleted");
                    DelegateVM.StationChangedEvent -= HandleAStationChanged;
                    //DelegateVM.NotifyStationChanged(Station.Id);
                    DelegateVM.NotifyStationChanged();
                    Tabs.CloseTab(param as TabItemFormat);
                }
            }
            catch (BO.ThereAreAssociatedOrgansException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }



    }
}
