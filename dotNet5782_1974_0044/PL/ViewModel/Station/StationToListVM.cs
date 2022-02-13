using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class StationToListVM : GenericList<StationToList>, IDisposable
    {
        public StationToListVM()
        {
            InitList();
            DelegateVM.StationChangedEvent += HandleStationChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        private async void InitList()
        {
            try
            {
                sourceList = new ObservableCollection<StationToList>( await PLService.GetStations());
                list = new ListCollectionView(sourceList);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
        private async void HandleStationChanged(object sender, EntityChangedEventArgs e)
        {
            try
            {
                if (e.Id != null)
                {
                    var station = sourceList.FirstOrDefault(s => s.Id == e.Id);
                    if (station != default)
                        sourceList.Remove(station);
                    var newStation =(await PLService.GetStations()).FirstOrDefault(s => s.Id == e.Id);
                    sourceList.Add(newStation);
                }
                else
                {
                    sourceList.Clear();
                    foreach (var item in await PLService.GetStations())
                        sourceList.Add(item);
                }
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
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

        public void Dispose()
        {
            DelegateVM.StationChangedEvent -= HandleStationChanged;
        }
    }
}