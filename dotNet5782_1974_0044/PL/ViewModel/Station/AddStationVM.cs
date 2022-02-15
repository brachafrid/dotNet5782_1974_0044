using PL.PO;
using System;
using System.Windows;

namespace PL
{
    public class AddStationVM : IDisposable
    {
        /// <summary>
        /// The added station   
        /// </summary>
        public StationAdd station { set; get; }
        /// <summary>
        /// Command of adding station
        /// </summary>
        public RelayCommand AddStationCommand { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public AddStationVM()
        {
            station = new();
            AddStationCommand = new(Add, param => station.Error == null && station.Location.Error == null);
        }

        /// <summary>
        /// Add station
        /// </summary>
        /// <param name="param"></param>
        public async void Add(object param)
        {
            try
            {
                await PLService.AddStation(station);
                DelegateVM.NotifyStationChanged(station.Id ?? 0);
                DelegateVM.NotifyDroneChanged();
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exsist");
                station.Id = null;
            }
        }
        /// <summary>
        /// Dispose the eventHandlers
        /// </summary>
        public void Dispose()
        {
        }
    }
}
