using PL.PO;
using System.Windows;

namespace PL
{
    public class AddStationVM
    {
        public StationAdd station { set; get; }
        public RelayCommand AddStationCommand { get; set; }
        public AddStationVM()
        {
            station = new();
            AddStationCommand = new(Add, param => station.Error == null && station.Location.Error == null);
        }
        public void Add(object param)
        {
            try
            {
                PLService.AddStation(station, () =>
                {
                    DelegateVM.NotifyStationChanged(station.Id ?? 0);
                    Tabs.CloseTab(param as TabItemFormat);
                });
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exsist");
                station.Id = null;
            }
        }

    }
}
