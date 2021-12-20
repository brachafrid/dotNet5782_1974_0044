
using System.Collections.Generic;
using System.Linq;
using Utilities;


namespace Dal
{
    //singelton pattern step #1 - recommend using "sealed" class to avoid derivated violation
    public sealed partial class DalObject:Singletone<DalObject>, DLApi.IDal
    {
        /// <summary>
        /// Call to quick initialization function
        /// </summary>
        DalObject()
        {
            DataSorce.Initialize(this);
        }

        /// <summary>
        /// Find if the id is exist in a spesific list
        /// </summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="lst">The spesific list </param>
        /// <param name="id">The id to check</param>
        static bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            if (!lst.Any())
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

