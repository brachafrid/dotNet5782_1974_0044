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
        public bool IsAdministor { get; set; }
        public ParcelToListVM()
        {
            sourceList = new ObservableCollection<ParcelToList>();
            list = new ListCollectionView(sourceList);
            IsAdministor = true;
            UpdateInitList();
            DelegateVM.Parcel += UpdateInitList;
            DelegateVM.Customer += UpdateInitList;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        public ParcelToListVM(object Id, object State)
        {
            id = (int)Id;
            IsAdministor = false;
            state = (string)State;
            UpdateInitList();
            DelegateVM.Customer += UpdateInitList;
            DelegateVM.Parcel += UpdateInitList;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }

        void UpdateInitList()
        {
            sourceList.Clear();
            switch (state)
            {
                case "From":
                    foreach (var item in PLService.GetCustomer((int)id).FromCustomer.Select(parcel => PLService.ConvertParcelAtCustomerToList(parcel)))
                        sourceList.Add(item);
                    break;
                case "To":
                    foreach (var item in PLService.GetCustomer((int)id).ToCustomer.Select(parcel => PLService.ConvertParcelAtCustomerToList(parcel)))
                        sourceList.Add(item);
                    break;
                default:
                    foreach (var item in PLService.GetParcels())
                        sourceList.Add(item);
                    break;
            }
        }

        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcel",
                Content = new AddParcelVM(true)
            });
        }
    }
}