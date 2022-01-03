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
        public RelayCommand AddDroneCommand { get; set; }
        public DroneToListVM()
        {
            UpdateInitList();
            DoubleClick = new(OpenDetails, null);
            DelegateVM.Drone += UpdateInitList;
            AddDroneCommand = new(AddDrone, null);
        }
        public void OpenDetails(object param)
        {
            if (param != null)
                Tabs.AddTab(new()
                {
                    TabContent = "UpdateDroneView",
                    Text = "Drone " + (param as DroneToList).Id,
                    Id = (param as DroneToList).Id

                });
        }

        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetDrones().ToList());
        }
        public void AddDrone(object param)
        {
            Tabs.AddTab(new()
            {
                TabContent = "AddDroneView",
                Text="Drone"
            });
        }
    }
}
