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
            UpdateInitList();
            DelegateVM.Parcel += UpdateInitList;
            DoubleClick = new(OpenDetails, null);
        }
        public ParcelToListVM(object Id)
        {
            object id = Id;
            UpdateInitListCustomer();
            DelegateVM.Parcel += UpdateInitListCustomer;
            DoubleClick = new(OpenDetails, null);
            list = new ListCollectionView(PLService.GetParcelsFrom((int)Id).ToList());

        }

        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetParcels().ToList());
        }
        void UpdateInitListCustomer()
        {
            
            //list = new ListCollectionView(PLService.GetParcels(Id: CustomerWindowVM.customer.Id).ToList());
        }
        public void OpenDetails(object param)
        {
            if (param != null)
                Tabs.AddTab(new()
                {
                    TabContent = "UpdateParcelView",
                    Text = "parcel " + (param as ParcelToList).Id,
                    Id = (param as ParcelToList).Id

                });
        }

        public override void AddEntity(object param)
        {
            Tabs.AddTab(new()
            {
                TabContent = "AddParcelView",
                Text = "Parcel"
            });
        }
    }
}