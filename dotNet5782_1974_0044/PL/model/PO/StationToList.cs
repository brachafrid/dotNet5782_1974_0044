

using System.ComponentModel;

namespace PL.PO
{
    public class StationToList : NotifyPropertyChangedBase
    {
        private int id;
        /// <summary>
        /// Station to list key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
        }
        private string name;
        /// <summary>
        /// Station to list name
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        private int chargeSlots;
        /// <summary>
        /// Station to list charge slots
        /// </summary>
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


