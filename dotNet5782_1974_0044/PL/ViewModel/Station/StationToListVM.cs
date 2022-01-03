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
        public RelayCommand AddStationCommand { get; set; }
        public StationToListVM()
        {
            UpdateInitList();
            DelegateVM.Station += UpdateInitList;
            AddStationCommand = new(AddStation, null);
        }
        void UpdateInitList()
        {
            list = new ListCollectionView(new StationHandler().GetStations().ToList());
        }
        public void AddStation(object param)
        {
            Tabs.TabItems.Add(new()
            {
                TabContent = "AddStationView",
                Text = "Station"
            });
        }
    }
}