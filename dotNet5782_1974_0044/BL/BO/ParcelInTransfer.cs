namespace BL
{
    namespace BO
    {
      public class ParcelInTransfer
        {
            public int Id { get; init; }
            public WeightCategories WeightCategory { get; set; }
            public Priorities Priority { get; set; }
            public bool ParcelState { get; set; }
            public Location CollectionPoint { get; set; }
            public Location DeliveryDestination { get; set; }
            public double TransportDistance { get; set; }
            public CustomerInParcel CustomerSender { get; set; }
            public CustomerInParcel CustomerReceives { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }

        }
    }

}
