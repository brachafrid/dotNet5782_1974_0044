using PL.PO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class DroneToListVM : GenericList<DroneToList>, IDisposable
    {
        /// <summary>
        /// constructor
        /// </summary>
        public DroneToListVM()
        {
            InitList();
            DoubleClick = new(Tabs.OpenDetailes, null);
            RefreshEvents.DroneChangedEvent += HandleDroneChanged;
        }

        /// <summary>
        /// Initialize a list of drones
        /// </summary>
        private async void InitList()
        {
            try
            {
            sourceList = new ObservableCollection<DroneToList>(await PLService.GetDrones());
            list = new ListCollectionView(sourceList);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Init Drones List", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Handle drone changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private async void HandleDroneChanged(object sender, EntityChangedEventArgs e)
        {
            try
            {
                if (e.Id != null)
                {
                    var drone = sourceList.FirstOrDefault(d => d.Id == e.Id);
                    if (drone != default)
                    {
                        sourceList.Remove(drone);
                    }
                    var newDrone = (await PLService.GetDrones()).FirstOrDefault(d => d.Id == e.Id);
                    sourceList.Add(newDrone);
                }
                else
                {
                    sourceList.Clear();
                    foreach (var item in await PLService.GetDrones())
                        sourceList.Add(item);
                }
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, "Init Drones List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="param"></param>
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                Header = "Drone",
                Content = new AddDroneVM()
            });
        }

        /// <summary>
        /// Dispose the eventHandles
        /// </summary>
        public void Dispose()
        {
            RefreshEvents.DroneChangedEvent -= HandleDroneChanged;
        }
    }
}
