﻿using PL.PO;
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
        ParcelListWindowState state = ParcelListWindowState.ALL;
        /// <summary>
        /// Is administor
        /// </summary>
        public bool IsAdministor { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public ParcelToListVM()
        {
            state = ParcelListWindowState.ALL;
            InitList();
            IsAdministor = true;
            RefreshEvents.CustomerChangedEvent += HandleParcelsChanged;
            RefreshEvents.ParcelChangedEvent += HandleParcelsChanged;
        }

        /// <summary>
        /// Initializes list of parcels
        /// </summary>
        private async void InitList()
        {
            try
            {
                
                sourceList = new ObservableCollection<ParcelToList>(await UpdateInitList());
                List = new ListCollectionView(sourceList);
                Count = (uint)List.Count;
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, $"Init Parcels List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Init Parcels List", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
            state = (ParcelListWindowState)State;
            InitList();
            RefreshEvents.CustomerChangedEvent += HandleParcelsChanged;
            RefreshEvents.ParcelChangedEvent += HandleParcelsChanged;
            DoubleClick = new(Tabs.OpenDetailes, null);
        }

        /// <summary>
        /// Handle parcel changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private async void HandleParcelsChanged(object sender, EntityChangedEventArgs e)
        {
            try
            {
                if (e.Id != null && e.Id != 0)
                {
                    var parcel = sourceList.FirstOrDefault(p =>p!=null && p.Id == e.Id);
                    if (parcel != null)
                    {
                        sourceList.Remove(parcel);
                        var newParcel = (await PLService.GetParcels()).FirstOrDefault(p => p.Id == e.Id);
                        if(newParcel!=null)
                            sourceList.Add(newParcel);
                    }
                }
                else
                {
                    sourceList.Clear();
                    switch (state)
                    {
                        case ParcelListWindowState.FROM_CUSTOMER:                            
                                foreach (var item in (await PLService.GetCustomer((int)customerId)).FromCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel)))
                                    sourceList.Add(await item);
                            break;
                        case ParcelListWindowState.TO_CUSTOMER:
                            foreach (var item in (await PLService.GetCustomer((int)customerId)).ToCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel)))
                                sourceList.Add(await item);
                            break;
                        default:
                            foreach (var item in await PLService.GetParcels())
                                sourceList.Add(item);
                            break;
                    }
                }
                Count = (uint)List.Count;
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Init Parcels List", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// return the state of the list of parcels
        /// </summary>
        /// <returns>state</returns>
        private async Task<IEnumerable<ParcelToList>> UpdateInitList()
        {
            try
            {
                return state switch
                {
                    ParcelListWindowState.FROM_CUSTOMER => await Task.WhenAll((await PLService.GetCustomer((int)customerId)).FromCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel))),
                    ParcelListWindowState.TO_CUSTOMER => await Task.WhenAll((await PLService.GetCustomer((int)customerId)).ToCustomer.Select(parcel => PlServiceConvert.ConvertParcelAtCustomerToList(parcel))),
                    ParcelListWindowState.ALL => await PLService.GetParcels()
                };
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message, $"Init Parcels List", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<ParcelToList>();
            }

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

        /// <summary>
        /// Dispose the eventHandlers
        /// </summary>
        public void Dispose()
        {
            RefreshEvents.CustomerChangedEvent -= HandleParcelsChanged;
            RefreshEvents.ParcelChangedEvent -= HandleParcelsChanged;
        }
    }
}