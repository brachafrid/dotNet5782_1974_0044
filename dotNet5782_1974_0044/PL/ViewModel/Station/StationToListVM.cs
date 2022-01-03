﻿using System;
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
            DoubleClick = new(OpenDetails, null);
            AddStationCommand = new(AddStation, null);
        }
        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetStations().ToList());
        }
        public void AddStation(object param)
        {
            Tabs.TabItems.Add(new()
            {
                TabContent = "AddStationView",
                Text = "Station"
            });
        }
        public void OpenDetails(object param)
        {
            if (param != null)
                Tabs.AddTab(new()
                {
                    TabContent = "UpdateStationView",
                    Text = "station " + (param as StationToList).Id,
                    Id = (param as StationToList).Id

                });
        }
    }
}