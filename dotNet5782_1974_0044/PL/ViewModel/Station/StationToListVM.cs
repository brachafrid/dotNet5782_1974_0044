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
            sourceList = new ObservableCollection<StationToList>();
            list = new ListCollectionView(sourceList);
            UpdateInitList();
            DelegateVM.Station += UpdateInitList;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        void UpdateInitList()
        {
            sourceList.Clear();
            foreach (var item in PLService.GetStations())
                sourceList.Add(item);
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