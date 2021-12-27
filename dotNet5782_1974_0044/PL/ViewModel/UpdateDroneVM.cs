using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModel
{
    public class UpdateDroneVM
    {


        //public int DroneModel
        //{
        //    get { return (string)GetValue(DroneModelProperty); }
        //    set { SetValue(DroneModelProperty, drone.Model); }
        //}

        //// Using a DependencyProperty as the backing store for DroneModel.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DroneModelProperty =
        //    DependencyProperty.Register("DroneModel", typeof(int), typeof(string), new PropertyMetadata(null));
        //private string droneModel;
        //public string DroneModel
        //{
        //    get
        //    {
        //        return this.droneModel;
        //    }
        //    set
        //    {
        //        this.droneModel = drone.Model;
        //        //RaisePropertyChanged(() => this.ViewModelItem);
        //    }
        //}

        public Drone drone { get; set; } = new();
        public RelayCommand UpdateDroneCommand { get; set; }
        public RelayCommand CloseDroneCommand { get; set; }
        public UpdateDroneVM()
        {
            UpdateDroneCommand = new(Update, param => drone.Error == null);
        }
        public void Update(object param)
        {
            new DroneHandler().UpdateDrone(drone.Id, param.ToString());
        }
       

    }
}
