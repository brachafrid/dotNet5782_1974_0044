using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    class UpdateParcelVM : NotifyPropertyChangedBase
    {
        private readonly int id;
        public RelayCommand OpenCustomerCommand { get; set; }
        public RelayCommand OpenDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }

        private Parcel parcel;

        public Parcel Parcel
        {
            get { return parcel; }
            set {
                Set(ref parcel, value);
            }
        }

        public RelayCommand DeleteParcelCommand { get; set; }

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
        private void HandleAParcelChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitParcel();
        }
        public async void InitParcel()
        {
            try
            {
                parcel = await PLService.GetParcel(id);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            //if (Parcel.AssignmentTime == null)
            //{
            //    Parcel.AssignmentTime = new DateTime();
            //}
            //if (Parcel.CollectionTime == null)
            //{
            //    Parcel.CollectionTime = new DateTime();
            //}
            //if (Parcel.DeliveryTime == null)
            //{
            //    Parcel.DeliveryTime = new DateTime();
            //}
        }
        public  async void DeleteParcel(object param)
        {
            try {
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

        public async void parcelTreatedByDrone(object param)
        {
            try
            {
                if (Parcel.AssignmentTime != null)
                {
                    if (Parcel.CollectionTime != null)
                    {
                      await  PLService.DeliveryParcelByDrone(Parcel.Drone.Id);
                        DelegateVM.NotifyDroneChanged(Parcel.Drone.Id);
                    }
                    else
                    {
                      await  PLService.ParcelCollectionByDrone(Parcel.Drone.Id);
                        DelegateVM.NotifyDroneChanged(Parcel.Drone.Id);
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
    }
}
