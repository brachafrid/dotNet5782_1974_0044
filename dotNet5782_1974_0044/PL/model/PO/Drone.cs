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
        /// <summary>
        /// drone key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string model;
        /// <summary>
        /// drone model
        /// </summary>
        [Required(ErrorMessage = "required")]
        public string Model
        {
            get => model;
            set => Set(ref model, value);
        }
        private WeightCategories weight;
        /// <summary>
        /// drone weight
        /// </summary>
        public WeightCategories Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private DroneState droneState;
        /// <summary>
        /// drone state
        /// </summary>
        public DroneState DroneState
        {
            get => droneState;
            set => Set(ref droneState, value);
        }
        private float battaryMode;
        /// <summary>
        /// drone battery mode
        /// </summary>
        public float BattaryMode
        {
            get => battaryMode;
            set => Set(ref battaryMode, value);
        }
        private Location location;
        /// <summary>
        /// drone location
        /// </summary>
        public Location Location
        {
            get => location;
            set => Set(ref location, value);
        }
        private ParcelInTransfer parcel;
        /// <summary>
        /// Parcel in transfer of the drone
        /// </summary>
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


