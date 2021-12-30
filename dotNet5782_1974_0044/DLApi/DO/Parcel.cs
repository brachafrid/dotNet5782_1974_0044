using System;



    namespace DO
    {
       public struct Parcel
       {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weigth { get; set; }
            public Priorities Priority { get; set; }
            public DateTime?  Requested { get; set; }//נוצר
            public int DorneId { get; set; }
            public DateTime?  Sceduled { get; set; }//שויך
            public DateTime?  PickedUp { get; set; }//נאסף
            public DateTime?  Delivered { get; set; }//סופק
            public bool IsDeleted { get; set; }

            public override string ToString()
                {
                    return $"Parcel ID:{Id} Sender:{SenderId} Target:{TargetId}";
                }

        }
    }



