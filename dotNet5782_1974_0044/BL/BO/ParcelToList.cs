using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace BO
    {
      public  class ParcelToList
        {
            public int Id { get; init; }
            public Customer CustomerSender { get; set; }
            public Customer CustomerReceives { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Piority { get; set; }
            public PackageModes PackageMode { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }

}
