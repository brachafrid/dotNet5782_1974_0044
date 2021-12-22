using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace PL.PO
    {
    public class CustomerToList : INotifyPropertyChanged
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
        private int numParcelSentDelivered;
        public int NumParcelSentDelivered 
        { 
            get => numParcelSentDelivered;
            set 
            {
                numParcelSentDelivered = value;
                onPropertyChanged("NumParcelSentDelivered");
            } 
        }
        private int numParcelSentNotDelivered;
        public int NumParcelSentNotDelivered
        {
            get => numParcelSentNotDelivered;
            set
            {
                numParcelSentNotDelivered = value;
                onPropertyChanged("NumParcelSentNotDelivered");
            }
        }

        private int numParcelReceived;
        public int NumParcelReceived
        {
            get => numParcelReceived;
            set
            {
                numParcelSentNotDelivered = value;
                onPropertyChanged("NumParcelReceived");
            }
        }

        private int numParcelWayToCustomer;
        public int NumParcelWayToCustomer
        {
            get => numParcelWayToCustomer;
            set
            {
                numParcelWayToCustomer = value;
                onPropertyChanged("NumParcelWayToCustomer");
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

