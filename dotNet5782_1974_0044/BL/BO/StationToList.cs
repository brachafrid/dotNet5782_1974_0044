

namespace BO
{
    public class StationToList
    {
        /// <summary> 
        /// Station to list key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Station to list name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Number full charge slots in station to list 
        /// </summary>
        public int FullChargeSlots { get; set; }
        /// <summary>
        /// Number empty charge slots in station to list 
        /// </summary>
        public int EmptyChargeSlots { get; set; }

        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }

}


