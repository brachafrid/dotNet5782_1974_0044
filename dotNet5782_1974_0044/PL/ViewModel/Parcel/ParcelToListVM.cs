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
            //object id = Id;
            // Customer customer = PLService.GetCustomer((int)Id);
            UpdateInitList();
            DelegateVM.Parcel += UpdateInitList;
            DelegateVM.Customer += UpdateInitList;
            DoubleClick = new(OpenDetails, null);
            //list = new ListCollectionView(customer.FromCustomer.ToList());
            //list = new ListCollectionView(PLService.GetCustomer((int)Id).FromCustomer.ToList());
            
        }

        void UpdateInitList()
        {
            list = new ListCollectionView(PLService.GetCustomer(7).FromCustomer.ToList());
            DelegateVM.Customer?.Invoke();
            DelegateVM.Parcel?.Invoke();

        }
        //void UpdateInitListCustomer()
        //{

        //    //list = new ListCollectionView(PLService.GetParcels(Id: CustomerWindowVM.customer.Id).ToList());
        //}
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