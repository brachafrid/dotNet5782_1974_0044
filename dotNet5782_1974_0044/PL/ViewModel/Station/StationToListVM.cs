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
            DoubleClick = new(OpenDetails, null);
        }
        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetStations().ToList());
        }
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                TabContent = "AddStationView",
                Text = "Station",
                Content = new AddStationVM()
            });
        }
        public void OpenDetails(object param)
        {
            if (param != null)
                Tabs.AddTab(new TabItemFormat()
                {
                    TabContent = "UpdateStationView",
                    Text = "station " + (param as StationToList).Id,
                    Id = (param as StationToList).Id,
                    Content = new UpdateStationVM((param as StationToList).Id)

                });
        }
    }
}