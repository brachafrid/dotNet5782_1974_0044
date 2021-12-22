

using System.ComponentModel;

namespace PL.PO
    {
       public class StationToList : INotifyPropertyChanged
    {
            public int Id { get; init; }
            public string Name { get; set; }
            public int FullChargeSlots { get; set; }
            public int EmptyChargeSlots { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }

    }


