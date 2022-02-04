using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PL.PO;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL
{
    public class DroneToListVM : GenericList<DroneToList>
    {
        public DroneToListVM()
        {
            sourceList = new ObservableCollection<DroneToList>(PLService.GetDrones());
            list = new ListCollectionView(sourceList);
            DoubleClick = new(Tabs.OpenDetailes, null);
            DelegateVM.DroneChangedEvent += HandleDroneChanged;
        }

        private void HandleDroneChanged(object sender, EntityChangedEventArgs e)
        {
            var drone = sourceList.FirstOrDefault(d => d.Id == e.Id);
            if (drone != default)
                sourceList.Remove(drone);
            var newDrone = PLService.GetDrones().FirstOrDefault(d => d.Id == e.Id);
            sourceList.Add(newDrone);
        }

        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                Header = "Drone",
                Content = new AddDroneVM()
            });
        }
    }
}
