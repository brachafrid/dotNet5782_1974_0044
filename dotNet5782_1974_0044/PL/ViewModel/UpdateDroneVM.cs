using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class UpdateDroneVM
    {
        public string droneModel;
        public Drone drone { get; set; }  
        public RelayCommand UpdateDroneCommand { get; set; }
        public RelayCommand CloseDroneCommand { get; set; }
        public RelayCommand SendChargingDroneCommand { get; set; }
        
        public UpdateDroneVM()
        {
            drone = new DroneHandler().GetDrone(2);
            UpdateDroneCommand = new(Update, param => drone.Error == null);
            SendChargingDroneCommand = new(sendToCharging, param => drone.Error == null);
            droneModel = drone.Model;
        }
        public void Update(object param)
        {
            new DroneHandler().UpdateDrone(drone.Id, droneModel);
            MessageBox.Show(drone.Model);
        }
        public void sendToCharging(object param)
        {
            new DroneHandler().SendDroneForCharg(drone.Id);
            MessageBox.Show($"{drone.DroneState}");
        }

    }
}
