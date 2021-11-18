﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
      public class CustomerInParcel
        {
            public int Id { get; init; }
            public string Name { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

}
