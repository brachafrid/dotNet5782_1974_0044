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
    public class ParcelToListVM : GenericList<ParcelToList>
    {
        public ParcelToListVM()
        {
            list = new ListCollectionView(new ParcelHandler().GetParcels().ToList());
        }
    }
}