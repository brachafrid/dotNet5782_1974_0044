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
        object id;
        string state = "";
        public ParcelToListVM()
        {
            UpdateInitList();
            DelegateVM.Parcel += UpdateInitList;
            DelegateVM.Customer += UpdateInitList;
            DoubleClick = new(OpenDetails, null);
        }
        public ParcelToListVM(object Id, object State)
        {
            id = Id;
            state = (string)State;
            UpdateInitList();
            DelegateVM.Customer += UpdateInitList;
            DelegateVM.Parcel += UpdateInitList;
            DoubleClick = new(OpenDetails, null);
        }
        void UpdateInitList()
        {
            if (state == "")
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
        public void OpenDetails(object param)
        {
            Type t = param.GetType();
            int id = (param is ParcelToList) ? (param as ParcelToList).Id : (param as ParcelAtCustomer).Id;
            if (param != null)
         
            Tabs.AddTab(new TabItemFormat()
                {
                    //TabContent = "UpdateParcelView",
                    Text = "parcel " + id,
                    Id = id,
                    Content= new UpdateParcelVM(id)

                });
        }

        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                //TabContent = "AddParcelView",
                Text = "Parcel",
                Content = new AddParcelVM()
            }) ;
        }
    }
}