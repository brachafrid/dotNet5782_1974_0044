using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelloList
        {
            public int Id { get; set; }
            public Customer CustomerSender { get; set; }
            public Customer CustomerReceives { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Piority { get; set; }
            public PackageModes PackageMode { get; set; }
        }
    }

}
