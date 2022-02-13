using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class Drone : NotifyPropertyChangedBase, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string model;
        [Required(ErrorMessage = "required")]
        public string Model
        {
            get => model;
            set => Set(ref model, value);
        }
        private WeightCategories weight;
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private DroneState droneState;
        public DroneState DroneState
        {
            get => droneState;
            set => Set(ref droneState, value);
        }
        private float battaryMode;
        public float BattaryMode
        {
            get => battaryMode;
            set => Set(ref battaryMode, value);
        }
        private Location location;
        public Location Location
        {
            get => location;
            set => Set(ref location, value);
        }
        private ParcelInTransfer parcel;
        public ParcelInTransfer Parcel 
        {
            get=>parcel;
            set => Set(ref parcel, value);
            
        }

        public string Error => Validation.ErorrCheck(this);

        public string this[string columnName] => Validation.PropError(columnName, this);
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


