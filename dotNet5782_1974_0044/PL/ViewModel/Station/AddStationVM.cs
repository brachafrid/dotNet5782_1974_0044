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
                RefreshEvents.NotifyStationChanged();
                RefreshEvents.NotifyDroneChanged();
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show(ex.Message+Environment.NewLine+$"The Id :{ex.Id}", "Adding Station", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
