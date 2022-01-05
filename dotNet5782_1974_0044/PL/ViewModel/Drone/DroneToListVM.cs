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
            UpdateInitList();
            DoubleClick = new(OpenDetails, null);
            DelegateVM.Drone += UpdateInitList;
        }
        public void OpenDetails(object param)
        {
            if (param != null)
                Tabs.AddTab(new()
                {
                    TabContent = "UpdateDroneView",
                    Text = "Drone " + (param as DroneToList).Id,
                    Id = (param as DroneToList).Id,
                    Content = new UpdateDroneView((param as DroneToList).Id)
                });
        }
        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetDrones().ToList());
        }
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                TabContent = "AddDroneView",
                Text = "Drone",
                Content = new AddDroneVM()
            }) ;
        }
    }
}
