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
            DataSorce.Initialize(this);
        }

        void uniqueIDTaxCheck<T>(List<T> lst, int id)
        {
            foreach (var item in lst)
            {
                if ((int)item.GetType().GetProperty("id").GetValue(item, null) == id)
                    throw new ArgumentException(" An element with the same key already exists in the list");
            }
        }

    }
  
}

   