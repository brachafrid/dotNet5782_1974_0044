using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace PL.PO
    {
    public class CustomerToList : NotifyPropertyChangedBase
    {
        private int id;
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string name;

        public string Name
        {
            get => name;
            set => Set(ref name, value);

        }
        private string phone;
        public string Phone
        {
            get => phone;
            set => Set(ref phone, value);
        }
        private int numParcelSentDelivered;
        public int NumParcelSentDelivered 
        { 
            get => numParcelSentDelivered;
            set => Set(ref numParcelSentDelivered, value); 
        }
        private int numParcelSentNotDelivered;
        public int NumParcelSentNotDelivered
        {
            get => numParcelSentNotDelivered;
            set => Set(ref numParcelSentNotDelivered, value);
        }

        private int numParcelReceived;
        public int NumParcelReceived
        {
            get => numParcelReceived;
            set => Set(ref numParcelReceived, value);
        }

        private int numParcelWayToCustomer;
        public int NumParcelWayToCustomer
        {
            get => numParcelWayToCustomer;
            set => Set(ref numParcelWayToCustomer, value);
        }

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

