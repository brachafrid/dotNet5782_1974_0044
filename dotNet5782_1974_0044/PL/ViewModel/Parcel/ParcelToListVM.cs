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
        public RelayCommand AddParcelCommand { get; set; }
        public ParcelToListVM()
        {
            UpdateInitList();
            DelegateVM.Parcel += UpdateInitList;
            DoubleClick = new(OpenDetails, null);
        }
        void UpdateInitList()
        {

            list = new ListCollectionView(PLService.GetParcels().ToList());
            AddParcelCommand=new(AddParcel, null);
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

        public void AddParcel(object param)
        {
            Tabs.TabItems.Add(new()
            {
                TabContent = "AddParcelView",
                Text = "Parcel"
            });
        }
    }
}