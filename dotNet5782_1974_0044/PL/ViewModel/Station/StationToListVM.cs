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
    public class StationToListVM : GenericList<StationToList>
    {
        public StationToListVM()
        {
            UpdateInitList();
            DelegateVM.Station += UpdateInitList;
        }
        void UpdateInitList()
        {
            list = new ListCollectionView(new StationHandler().GetStations().ToList());
        }
    }
}