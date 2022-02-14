using PL.PO;

using System;
using System.Collections.Generic;
using System.Windows;

namespace PL
{
    class UpdateParcelVM : NotifyPropertyChangedBase, IDisposable
    {
        private readonly int id;
        public RelayCommand OpenCustomerCommand { get; set; }
        public RelayCommand OpenDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }

        private Parcel parcel;

        public Parcel Parcel
        {
            get { return parcel; }
            set
            {
                Set(ref parcel, value);
            }
        }

        public RelayCommand DeleteParcelCommand { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id"></param>
        public UpdateParcelVM(int id)
        {
            this.id = id;
            InitParcel();
            DeleteParcelCommand = new(DeleteParcel, param => Parcel?.Error == null);
            OpenCustomerCommand = new(Tabs.OpenDetailes, null);
            OpenDroneCommand = new(Tabs.OpenDetailes, null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => Parcel?.Error == null);
            DelegateVM.ParcelChangedEvent += HandleAParcelChanged;
        }

        /// <summary>
        /// Handle parcel changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void HandleAParcelChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitParcel();
        }

        /// <summary>
        /// Initializes a parcel
        /// </summary>
        public async void InitParcel()
        {
            try
            {
                Parcel = await PLService.GetParcel(id);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }

        /// <summary>
        /// Delete parcel
        /// </summary>
        /// <param name="param"></param>
        public async void DeleteParcel(object param)
        {
            try
            {
                if (MessageBox.Show("You're sure you want to delete this parcel?", "Delete Parcel", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    await PLService.DeleteParcel(Parcel.Id);
                    MessageBox.Show("The parcel was successfully deleted");
                    DelegateVM.ParcelChangedEvent -= HandleAParcelChanged;
                    DelegateVM.NotifyParcelChanged(Parcel.Id);
                    Tabs.CloseTab(param as TabItemFormat);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }

        /// <summary>
        /// parcel treated by drone
        /// </summary>
        /// <param name="param"></param>
        public async void parcelTreatedByDrone(object param)
        {
            try
            {
                if (Parcel.AssignmentTime != null)
                {
                    if (Parcel.CollectionTime != null)
                    {
                        await PLService.DeliveryParcelByDrone(Parcel.Drone.Id);
                        DelegateVM.NotifyDroneChanged(Parcel.Drone.Id);
                        DelegateVM.NotifyParcelChanged(Parcel.Id);
                    }
                    else
                    {
                        await PLService.ParcelCollectionByDrone(Parcel.Drone.Id);
                        DelegateVM.NotifyDroneChanged(Parcel.Drone.Id);
                        DelegateVM.NotifyParcelChanged(Parcel.Id);
                    }
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.InvalidParcelStateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }

        public void Dispose()
        {
            DelegateVM.ParcelChangedEvent -= HandleAParcelChanged;
        }
    }
}
