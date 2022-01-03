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
   public class DroneToListVM:GenericList<DroneToList>
    {
        public DroneToListVM()
        {
            UpdateInitList();
            DelegateVM.Drone += UpdateInitList;
        }

         void UpdateInitList()
        {
            list = new ListCollectionView(new DroneHandler().GetDrones().ToList());
        }
    }
}
