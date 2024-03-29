﻿using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class StationToListVM : GenericList<StationToList>, IDisposable
    {
        /// <summary>
        /// constructor
        /// </summary>
        public StationToListVM()
        {
            InitList();
            RefreshEvents.StationChangedEvent += HandleStationChanged;
        }

        /// <summary>
        /// Initializes list of stations
        /// </summary>
        private async void InitList()
        {
            try
            {
                sourceList = new ObservableCollection<StationToList>(await PLService.GetStations());
                List = new ListCollectionView(sourceList);
                Count = (uint)List.Count;
                if (List.Count == 0)
                    Count = 0;
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Init Stations List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handle station changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private async void HandleStationChanged(object sender, EntityChangedEventArgs e)
        {
            try
            {
                if (e.Id != null)
                {
                    var station = sourceList.FirstOrDefault(s => s.Id == e.Id);
                    if (station != default)
                    {
                        sourceList.Remove(station);
                        var newStation = (await PLService.GetStations()).FirstOrDefault(s => s.Id == e.Id);
                        if (newStation != null)
                            sourceList.Add(newStation);
                    }
               
                }
                else
                {
                    sourceList.Clear();
                    foreach (var item in await PLService.GetStations())
                        sourceList.Add(item);
                }
                Count = (uint)List.Count;
                if (List.Count == 0)
                    Count = 0;
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Init Stations List", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="param"></param>
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Station",
                Content = new AddStationVM()
            });
        }

        /// <summary>
        /// Dispose the eventHandlers
        /// </summary>
        public void Dispose()
        {
            RefreshEvents.StationChangedEvent -= HandleStationChanged;
        }
    }
}