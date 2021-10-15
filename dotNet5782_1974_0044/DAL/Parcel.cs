﻿using System;
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
            public Prioripies Priority { get; set; }
            public DateTime Requested { get; set; }
            public int DorneId { get; set; }
            public DateTime Sceduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                return $"Parcel ID:{Id} Sender:{SenderId} Target:{TargetId}";
            }

        }
    }


}
