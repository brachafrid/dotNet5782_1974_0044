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
        int? customerId =null;
        string state = string.Empty;
        public bool IsAdministor { get; set; }
        public ParcelToListVM()
        {
            sourceList = new ObservableCollection<ParcelToList>(UpdateInitList());
            list = new ListCollectionView(sourceList);
            IsAdministor = true;
            UpdateInitList();
            DelegateVM.CustomerChangedEvent += HandleParcelChanged;
            DelegateVM.ParcelChangedEvent += HandleParcelChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        public ParcelToListVM(object Id, object State)
        {
            customerId = (int)Id;
            IsAdministor = false;
            state = (string)State;
            sourceList = new ObservableCollection<ParcelToList>(UpdateInitList());
            list = new ListCollectionView(sourceList);
            //UpdateInitList();
            DelegateVM.CustomerChangedEvent += HandleParcelChanged;
            DelegateVM.ParcelChangedEvent += HandleParcelChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }
        private void HandleParcelChanged(object sender, EntityChangedEventArgs e)
        {
            if (e.Id != null)
            {
                var parcel = sourceList.FirstOrDefault(p => p.Id == e.Id);
                if (parcel != default)
                    sourceList.Remove(parcel);
                var newParcel = PLService.GetParcels().FirstOrDefault(p => p.Id == e.Id);
                sourceList.Add(newParcel);
            }
            else
            {
                sourceList.Clear();
                switch (state)
                {
                    case "From":
                        foreach (var item in PLService.GetCustomer((int)customerId).FromCustomer.Select(parcel => PLService.ConvertParcelAtCustomerToList(parcel)))
                            sourceList.Add(item);
                        break;
                    case "To":
                        foreach (var item in PLService.GetCustomer((int)customerId).ToCustomer.Select(parcel => PLService.ConvertParcelAtCustomerToList(parcel)))
                            sourceList.Add(item);
                        break;
                    default:
                        foreach (var item in PLService.GetParcels())
                            sourceList.Add(item);
                        break;
                }
            }
        }

       private IEnumerable<ParcelToList> UpdateInitList()
        {
            return state switch
            {
                "From" => PLService.GetCustomer((int)customerId).FromCustomer.Select(parcel => PLService.ConvertParcelAtCustomerToList(parcel)),
                "To" => PLService.GetCustomer((int)customerId).ToCustomer.Select(parcel => PLService.ConvertParcelAtCustomerToList(parcel)),
                _ => PLService.GetParcels()
            };           
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