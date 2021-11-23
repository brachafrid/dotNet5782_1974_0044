using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Call to quick initialization function
        /// </summary>
        public DalObject()
        {
            DataSorce.Initialize(this);
        }

        /// <summary>
        /// Find if the id is exist in a spesific list
        /// </summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="lst">The spesific list </param>
        /// <param name="id">The id to check</param>
        bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            if (lst.Count() <= 0)
                return false;
           T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);
            
            return !(temp.Equals(default(T)));
        }
        /// <summary>
        /// Takes from the DataSource the electricity use data of the drone
        /// </summary>
        /// <returns>A array of electricity use</returns>

        public (double, double,double,double,double) GetElectricity()
        {
            return (DataSorce.Config.Available, DataSorce.Config.LightWeightCarrier, DataSorce.Config.MediumWeightBearing, DataSorce.Config.CarriesHeavyWeight, DataSorce.Config.DroneLoadingRate);
    
        }

    }

}

