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
            sourceList = new ObservableCollection<DroneToList>();
            list = new ListCollectionView(sourceList);
            UpdateInitList();
           
            DoubleClick = new(Tabs.OpenDetailes, null);
            DelegateVM.Drone += UpdateInitList;
        }
        void UpdateInitList()
        {
            sourceList.Clear();
            foreach (var item in PLService.GetDrones())
                sourceList.Add(item);
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
