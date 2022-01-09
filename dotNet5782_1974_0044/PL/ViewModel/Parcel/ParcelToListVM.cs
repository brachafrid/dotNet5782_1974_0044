using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using PL.PO;
using System.Collections.ObjectModel;

namespace PL
{
    public class ParcelToListVM : GenericList<ParcelToList>
    {
        int? id =null;
        string state = string.Empty;
        public ParcelToListVM()
        {
            UpdateInitList();
            DelegateVM.Parcel += UpdateInitList;
            DelegateVM.Customer += UpdateInitList;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        public ParcelToListVM(object Id, object State)
        {
            id = (int)Id;
            state = (string)State;
            UpdateInitList();
            DelegateVM.Customer += UpdateInitList;
            DelegateVM.Parcel += UpdateInitList;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }

        void UpdateInitList()
        {
           
            if (state == string.Empty)
            {
                list = new ListCollectionView(PLService.GetParcels().ToList());
            }
            if (state == "From")
            {
                list = new ListCollectionView(PLService.GetCustomer((int)id).FromCustomer.ToList());
            }
            if (state == "To")
            {
                list = new ListCollectionView(PLService.GetCustomer((int)id).ToCustomer.ToList());
            }
        }
     

        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcel",
                Content = new AddParcelVM()
            }) ;
        }
    }
}