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
        public RelayCommand ChargingDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }
        
        public UpdateDroneVM()
        {
            drone = new DroneHandler().GetDrone(2);
            UpdateDroneCommand = new(Update, param => drone.Error == null);
            ChargingDroneCommand = new(sendToCharging, param => drone.Error == null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => drone.Error == null);
            droneModel = drone.Model;
        }
        public void Update(object param)
        {
            new DroneHandler().UpdateDrone(drone.Id, droneModel);
            drone = new DroneHandler().GetDrone(2);

            MessageBox.Show(drone.Model);
        }
        public void sendToCharging(object param)
        {
            if(drone.DroneState == DroneState.AVAILABLE)
            {
                new DroneHandler().SendDroneForCharg(drone.Id);
                drone = new DroneHandler().GetDrone(2);

                MessageBox.Show($"{drone.DroneState}");
            }
            if(drone.DroneState == DroneState.MAINTENANCE)
            {
                new DroneHandler().ReleaseDroneFromCharging(drone.Id);
                drone = new DroneHandler().GetDrone(2);

            }
        }
        public void parcelTreatedByDrone(object param)
        {
            if (drone.DroneState == DroneState.DELIVERY)
            {
                if (drone.Parcel.ParcelState == true)
                {
                    new DroneHandler().DeliveryParcelByDrone(drone.Id);
                    drone = new DroneHandler().GetDrone(2);

                }
                else
                {
                    new DroneHandler().ParcelCollectionByDrone(drone.Id);
                    drone = new DroneHandler().GetDrone(2);
                }

            }
            else
            {
                new DroneHandler().AssingParcelToDrone(drone.Id);
                drone = new DroneHandler().GetDrone(2);
            }

        }
    }
}
