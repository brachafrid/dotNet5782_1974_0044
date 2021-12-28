using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneAdd : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? id;
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Id
        {
            get => id;
            set
            {
                id = value;
                onPropertyChanged("Id");
            }
        }

        private int stationId;
        [Required(ErrorMessage = "required")]
        public int StationId
        {
            get { return stationId; }
            set { stationId = value; }
        }

        private string model;
        [Required(ErrorMessage = "required")]
        public string Model
        {
            get => model;
            set
            {
                model = value;
                onPropertyChanged("Model");
            }
        }

        private WeightCategories? weight;
        [Required(ErrorMessage = "required")]
        public WeightCategories? Weight
        {
            get => weight;
            set
            {
                weight = value;
                onPropertyChanged("Weight");
            }
        }

        private DroneState droneState;
        [Required(ErrorMessage = "required")]
        public DroneState DroneState
        {
            get => droneState;
            set
            {
                droneState = value;
                onPropertyChanged("DroneState");
            }
        }
        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);
        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
        }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


