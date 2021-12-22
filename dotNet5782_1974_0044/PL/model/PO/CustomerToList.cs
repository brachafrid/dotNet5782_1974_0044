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
            public int Id { get; init; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int NumParcelSentDelivered { get; set; }
            public int NumParcelSentNotDelivered { get; set; }
            public int NumParcelReceived { get; set; }
            public int NumParcelWayToCustomer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

