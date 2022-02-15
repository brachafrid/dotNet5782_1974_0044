using System;


namespace BO
{
    public class Parcel
    {
        /// <summary>
        /// Parcel id
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Customer sender of the parcel
        /// </summary>
        public CustomerInParcel CustomerSender { get; set; }
        /// <summary>
        /// Customer receives of the parcel
        /// </summary>
        public CustomerInParcel CustomerReceives { get; set; }
        /// <summary>
        /// Parcel weight
        /// </summary>
        public WeightCategories Weight { get; set; }
        /// <summary>
        /// Parcel priority
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// The drone of the parcel
        /// </summary>
        public DroneWithParcel Drone { get; set; }
        /// <summary>
        /// Parcel creation time
        /// </summary>
        public DateTime? CreationTime { get; set; }
        /// <summary>
        /// Parcel assignment time
        /// </summary>
        public DateTime? AssignmentTime { get; set; }
        /// <summary>
        /// Parcel collection time
        /// </summary>
        public DateTime? CollectionTime { get; set; }
        /// <summary>
        /// Parcel delivery time
        /// </summary>
        public DateTime? DeliveryTime { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}


