using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class CustomerAdd : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get => id;
            init
            {
                id = value;
                onPropertyChanged("Id");
            }
        }
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                onPropertyChanged("Name");
            }
        }
        private string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                onPropertyChanged("Phone");
            }
        }

        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                location = value;
                onPropertyChanged("CollectionPoint");
            }
        }
        public string Error => "invalid parameter";

        public string this[string columnName] => Validation.functions.FirstOrDefault(func => func.Key == columnName.GetType()).Value(this.GetType().GetProperty(columnName).GetValue(this)) ? null : "invalid " + columnName;

        public override string ToString()
        {
            return this.ToStringProperties();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}


