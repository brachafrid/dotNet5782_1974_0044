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
        /// <summary>
        /// customer to list key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string name;
        /// <summary>
        /// customer to list name
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);

        }
        private string phone;
        /// <summary>
        /// customer to list phone
        /// </summary>
        public string Phone
        {
            get => phone;
            set => Set(ref phone, value);
        }
        private int numParcelSentDelivered;
        /// <summary>
        /// Number of parcels sent and delivered
        /// </summary>
        public int NumParcelSentDelivered 
        { 
            get => numParcelSentDelivered;
            set => Set(ref numParcelSentDelivered, value); 
        }
        private int numParcelSentNotDelivered;
        /// <summary>
        /// Number of parcels sent and not delivered
        /// </summary>
        public int NumParcelSentNotDelivered
        {
            get => numParcelSentNotDelivered;
            set => Set(ref numParcelSentNotDelivered, value);
        }

        private int numParcelReceived;
        /// <summary>
        /// Number of parcels received
        /// </summary>
        public int NumParcelReceived
        {
            get => numParcelReceived;
            set => Set(ref numParcelReceived, value);
        }

        private int numParcelWayToCustomer;
        /// <summary>
        /// Number of parcels are in the way to customer
        /// </summary>
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

