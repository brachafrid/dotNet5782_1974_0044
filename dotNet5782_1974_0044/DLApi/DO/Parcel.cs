
using System;



namespace DO
{
    public struct Parcel : IIdentifyable, IActiveable
    {
        /// <summary>
        /// The Parcel id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The id of the parcel`s sender
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// The id of the parcel`s reciver
        /// </summary>
        public int TargetId { get; set; }
        /// <summary>
        /// the weight of the parcel
        /// </summary>
        public WeightCategories Weigth { get; set; }
        /// <summary>
        /// The piority of the parcel
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// The time that the parcel created
        /// </summary>
        public DateTime? Requested { get; set; }
        /// <summary>
        /// The drone that take the parcel
        /// </summary>
        public int DorneId { get; set; }
        /// <summary>
        /// The time the parcel assign to drone
        /// </summary>
        public DateTime? Sceduled { get; set; }
        /// <summary>
        /// The time the parcel picke up from the sender
        /// </summary>
        public DateTime? PickedUp { get; set; }
        /// <summary>
        /// The time the customer arrive to the resiver
        /// </summary>
        public DateTime? Delivered { get; set; }
        /// <summary>
        /// Is the parcel active
        /// </summary>
        public bool IsNotActive { get; set; }

        public override string ToString()
        {
            return $"Parcel ID:{Id} Sender:{SenderId} Target:{TargetId}";
        }

    }
}



