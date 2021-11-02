using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class StationToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int FullChargeSlots { get; set; }
            public int EmptyChargeSlots { get; set; }
        }
    }
}

