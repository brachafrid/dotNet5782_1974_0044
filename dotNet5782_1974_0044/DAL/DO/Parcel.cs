using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
       public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weigth { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }//נוצר
            public int DorneId { get; set; }
            public DateTime Sceduled { get; set; }//שויך
            public DateTime PickedUp { get; set; }//נאסף
            public DateTime Delivered { get; set; }//סופק
            public override string ToString()
            {
                return $"Parcel ID:{Id} Sender:{SenderId} Target:{TargetId}";
            }

        }
    }


}
