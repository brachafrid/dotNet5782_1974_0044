using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class DroneAdd : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? id;
        public int? Id
        {
            get => id;
            init
            {
                id = value;
                onPropertyChanged("Id");
            }
        }
        private string model;
        private int stationId;

        public int StationId
        {
            get { return stationId; }
            set { stationId = value; }
        }

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
        public DroneState DroneState
        {
            get => droneState;
            set
            {
                droneState = value;
                onPropertyChanged("DroneState");
            }
        }
        
        public string Error
        {
            get
            {
                foreach (var item in GetType().GetProperties())
                {
                    if(this[nameof(item)] != null)
                        return "invalid" + item.Name;
                }
                return 
                    null;
            }
        }

        public string this[string columnName] {
            get
            {
                //var func = Validation.functions.FirstOrDefault(func => func.Key == GetType().GetProperty(columnName).GetType());
                //if(!func.Equals(default))
                //   return func.Value(GetType().GetProperty(columnName).GetValue(this)) ? null : "invalid " + columnName;
                return null;

            }
        } 

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


