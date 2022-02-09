using PL.PO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace PL
{
    public class DroneToListVM : GenericList<DroneToList>
    {
        public DroneToListVM()
        {
            InitList();
            DoubleClick = new(Tabs.OpenDetailes, null);
            DelegateVM.DroneChangedEvent += HandleDroneChanged;
        }
        private async void InitList()
        {
            sourceList = new ObservableCollection<DroneToList>(await PLService.GetDrones());
            list = new ListCollectionView(sourceList);
        }
        private async void HandleDroneChanged(object sender, EntityChangedEventArgs e)
        {
            if (e.Id != null)
            {
                var drone = sourceList.FirstOrDefault(d => d.Id == e.Id);
                if (drone != default)
                    sourceList.Remove(drone);
                var newDrone = (await PLService.GetDrones()).FirstOrDefault(d => d.Id == e.Id);
                sourceList.Add(newDrone);
            }
            else
            {
                sourceList.Clear();
                foreach (var item in (await PLService.GetDrones()))
                    sourceList.Add(item);
            }
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
