using PL.PO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace PL
{
    public class StationToListVM : GenericList<StationToList>
    {
        public StationToListVM()
        {
            sourceList = new ObservableCollection<StationToList>(PLService.GetStations());
            list = new ListCollectionView(sourceList);
            DelegateVM.StationChangedEvent += HandleStationChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        private void HandleStationChanged(object sender, EntityChangedEventArgs e)
        {
            if (e.Id != null)
            {
                var station = sourceList.FirstOrDefault(s => s.Id == e.Id);
                if (station != default)
                    sourceList.Remove(station);
                var newStation = PLService.GetStations().FirstOrDefault(s => s.Id == e.Id);
                sourceList.Add(newStation);
            }
            else
            {
                sourceList.Clear();
                foreach (var item in PLService.GetStations())
                    sourceList.Add(item);
            }
        }

        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Station",
                Content = new AddStationVM()
            });
        }

    }
}