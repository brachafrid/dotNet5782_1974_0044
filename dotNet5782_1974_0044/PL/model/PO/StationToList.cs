

using System.ComponentModel;

namespace PL.PO
{
    public class StationToList : NotifyPropertyChangedBase
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
        private int chargeSlots;
        public int ChargeSlots {
            get => chargeSlots;
            set => Set(ref chargeSlots, value);
        }
     
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }

}


