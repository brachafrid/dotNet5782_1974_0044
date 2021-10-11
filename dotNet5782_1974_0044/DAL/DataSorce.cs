using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
 

namespace DalObject
{
    class DataSorce
    {
        internal static Drone[] drones = new Drone[10];
        internal static Station[] stations = new Station[5];
        internal static Customer[] customers = new Customer[100];
        internal static Parcel[] parcels = new Parcel[1000];

        internal class Config
        {
            internal static int idxDrones = 0;
            internal static int idxStations=0;
            internal static int idxCustomers=0;
            internal static int idxParcels=0;

            static int IdParcel =0;
        }

        static void Initialize()
        {
               
        }
    }

}

