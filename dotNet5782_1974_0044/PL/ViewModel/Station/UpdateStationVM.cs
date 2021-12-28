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



        public Station station
        {
            get { return (Station)GetValue(stationProperty); }
            set { SetValue(stationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationProperty =
            DependencyProperty.Register("station", typeof(Station), typeof(UpdateStationVM), new PropertyMetadata(new Station()));



        public string stationName
        {
            get { return (string)GetValue(stationNameProperty); }
            set { SetValue(stationNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationNameProperty =
            DependencyProperty.Register("stationName", typeof(string), typeof(UpdateCustomerVM), new PropertyMetadata(""));



        public int stationEmptyChargeSlots
        {
            get { return (int)GetValue(stationEmptyChargeSlotsProperty); }
            set { SetValue(stationEmptyChargeSlotsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customerPhone.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationEmptyChargeSlotsProperty =
            DependencyProperty.Register("stationEmptyChargeSlots", typeof(int), typeof(UpdateCustomerVM), new PropertyMetadata(0));


        public RelayCommand UpdateStationCommand { get; set; }

        public UpdateStationVM()
        {
            init();
            stationName = station.Name;
            stationEmptyChargeSlots = station.EmptyChargeSlots;

            UpdateStationCommand = new(UpdateStation, param => station.Error == null);
            DelegateVM.Station += init;
        }
        public void init()
        {
            station = new StationHandler().GetStation(2);
        }
        public void UpdateStation(object param)
        {
            if (stationName != station.Name || stationEmptyChargeSlots != station.EmptyChargeSlots)
            {
                new StationHandler().UpdateStation(station.Id, station.Name, station.EmptyChargeSlots);
                stationName = station.Name;
                stationEmptyChargeSlots = station.EmptyChargeSlots;
            }

        }

    }
}
