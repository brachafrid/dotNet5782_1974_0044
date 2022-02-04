using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PL.PO;
using System.Collections.ObjectModel;

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
            var station = sourceList.FirstOrDefault(s => s.Id == e.Id);
            if (station != default)
                sourceList.Remove(station);
            var newStation = PLService.GetStations().FirstOrDefault(s => s.Id == e.Id);
            sourceList.Add(newStation);
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