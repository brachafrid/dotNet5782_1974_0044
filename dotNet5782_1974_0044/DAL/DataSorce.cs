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
        const int DRONE_LENGTH = 10;
        const int STATIONS_LENGTH = 5;
        const int CUSTOMERS_LENGTH = 100;
        const int PARCELS_LENGTH = 1000;
        internal static Drone[] drones = new Drone[DRONE_LENGTH];
        internal static Station[] stations = new Station[STATIONS_LENGTH];
        internal static Customer[] customers = new Customer[CUSTOMERS_LENGTH];
        internal static Parcel[] parcels = new Parcel[PARCELS_LENGTH];

        internal class Config
        {
            internal static int idxDrones = 0;
            internal static int idxStations=0;
            internal static int idxCustomers=0;
            internal static int idxParcels=0;

            static int IdParcel =0;
        }

        static internal void Initialize()
        {
               
        }
    }

}

