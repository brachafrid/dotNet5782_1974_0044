namespace BO
{
    public class CustomerToList
    {
        /// <summary>
        /// Customer to list key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Customer to list name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Customer to list phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Number parcels of the customer that sent and delivered 
        /// </summary>
        public int NumParcelSentDelivered { get; set; }
        /// <summary>
        /// Number parcels of the customer that sent and and delivered 
        /// </summary>
        public int NumParcelSentNotDelivered { get; set; }
        /// <summary>
        /// Number parcels of the customer that recived 
        /// </summary>
        public int NumParcelReceived { get; set; }
        /// <summary>
        /// Number parcels that in way to customer
        /// </summary>
        public int NumParcelWayToCustomer { get; set; }
        /// <summary>
        /// If the customer is not active
        /// </summary>
        public bool IsNotActive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}

