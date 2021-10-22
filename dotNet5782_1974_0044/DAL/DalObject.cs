using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
     public partial class DalObject
    {
        /// <summary>
        /// Call to quick initialization function
        /// </summary>
        public DalObject()
        {
           
            DataSorce.Initialize();
        }

    }
  
}

   