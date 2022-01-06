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
            DoubleClick = new(Tabs.OpenDroneDetails, null);
            DelegateVM.Drone += UpdateInitList;
        }
        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetDrones().ToList());
        }
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                Text = "Drone",
                Content = new AddDroneVM()
            }) ;
        }
    }
}
