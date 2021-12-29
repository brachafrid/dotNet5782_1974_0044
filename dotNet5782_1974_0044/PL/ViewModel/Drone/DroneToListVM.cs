using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PL.PO;
using System.Collections.ObjectModel;

namespace PL
{
   public class DroneToListVM:GenericList<Drone>
    {
        //public ListCollectionView list { set; get; }
        //public ObservableCollection<string> SortOption { set; get; }
        public DroneToListVM()
        {
            //SortOption = new ObservableCollection<string>(typeof(DroneToList).GetProperties().Where(prop => prop.PropertyType.IsEnum).Select(prop => prop.Name).ToList());
            //list = new ListCollectionView(new DroneHandler().GetDrones().ToList());
        }
    }
}
