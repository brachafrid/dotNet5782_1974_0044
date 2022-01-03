﻿using System;
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
            DelegateVM.Drone += UpdateInitList;
            AddDroneCommand = new(AddDrone, null);
        }

        void UpdateInitList()
        {
            list = new ListCollectionView(new DroneHandler().GetDrones().ToList());
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
