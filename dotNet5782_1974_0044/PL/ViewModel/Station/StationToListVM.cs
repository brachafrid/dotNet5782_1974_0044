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
            UpdateInitList();
            DelegateVM.Station += UpdateInitList;
            DoubleClick = new(Tabs.OpenStationDetails, null);
        }
        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetStations().ToList());
        }
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Text = "Station",
                Content = new AddStationVM()
            });
        }
     
    }
}