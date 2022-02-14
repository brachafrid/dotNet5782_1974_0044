using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class ParcelToListVM : GenericList<ParcelToList>, IDisposable
    {
        int? customerId = null;
        string state = string.Empty;
        public bool IsAdministor { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public ParcelToListVM()
        {
            InitList();
            IsAdministor = true;
            DelegateVM.CustomerChangedEvent += HandleParcelChanged;
            DelegateVM.ParcelChangedEvent += HandleParcelChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }

        /// <summary>
        /// Initializes list of parcels
        /// </summary>
        private async void InitList()
        {
            sourceList = new ObservableCollection<ParcelToList>(await UpdateInitList());
            list = new ListCollectionView(sourceList);
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="Id">id</param>
        /// <param name="State">state</param>
        public ParcelToListVM(object Id, object State)
        {
            customerId = (int)Id;
            IsAdministor = false;
            state = (string)State;
            InitList();
            //list = new ListCollectionView(sourceList);
            DelegateVM.CustomerChangedEvent += HandleParcelChanged;
            DelegateVM.ParcelChangedEvent += HandleParcelChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }

        /// <summary>
        /// Handle parcel changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private async void HandleParcelChanged(object sender, EntityChangedEventArgs e)
        {
            if (e.Id != null && e.Id != 0)
            {
                var parcel = sourceList.FirstOrDefault(p => p.Id == e.Id);
                if (parcel != default)
                    sourceList.Remove(parcel);
                var newParcel = (await PLService.GetParcels()).FirstOrDefault(p => p.Id == e.Id);
                sourceList.Add(newParcel);
            }
            else
            {
                sourceList.Clear();
                switch (state)
                {
                    case "From":
                        {
                            foreach (var item in (await PLService.GetCustomer((int)customerId)).FromCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel)))
                                sourceList.Add(await item);
                        }
                        break;
                    case "To":
                        foreach (var item in (await PLService.GetCustomer((int)customerId)).ToCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel)))
                            sourceList.Add(await item);
                        break;
                    default:
                        foreach (var item in await PLService.GetParcels())
                            sourceList.Add(item);
                        break;
                }
            }
        }

        /// <summary>
        /// return the state of the list of parcels
        /// </summary>
        /// <returns>state</returns>
        private async Task<IEnumerable<ParcelToList>> UpdateInitList()
        {
            //try
            //{
                return state switch
                {
                    "From" => await Task.WhenAll((await PLService.GetCustomer((int)customerId)).FromCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel))),
                    "To" => await Task.WhenAll((await PLService.GetCustomer((int)customerId)).ToCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel))),
                    _ => await PLService.GetParcels()
                };
            //}
            //catch (KeyNotFoundException ex)
            //{
            //    MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            //    throw new KeyNotFoundException();
            //}
            //catch (BO.XMLFileLoadCreateException ex)
            //{
            //    MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            //    throw new KeyNotFoundException();
            //}
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="param"></param>
        public override void AddEntity(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcel",
                Content = new AddParcelVM(true)
            });
        }

        public void Dispose()
        {
            DelegateVM.CustomerChangedEvent -= HandleParcelChanged;
            DelegateVM.ParcelChangedEvent -= HandleParcelChanged;
        }
    }
}