
namespace IBL
{
    namespace BO
    {
       public class StationToList
        {
            public int Id { get; init; }
            public string Name { get; set; }
            public int FullChargeSlots { get; set; }
            public int EmptyChargeSlots { get; set; }

            public override string ToString()
            {
                return this.ToStringProperties();
            }
        }

    }
}

