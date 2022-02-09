using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    class UpdateParcelVM : DependencyObject
    {
        private readonly int id;
        public RelayCommand OpenCustomerCommand { get; set; }
        public RelayCommand OpenDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }
 

        public Parcel parcel
        {
            get { return (Parcel)GetValue(parcelProperty); }
            set { SetValue(parcelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty parcelProperty =
            DependencyProperty.Register("parcel", typeof(Parcel), typeof(UpdateParcelVM), new PropertyMetadata(new Parcel()));

        public RelayCommand DeleteParcelCommand { get; set; }

        public UpdateParcelVM(int id)
        {
            this.id = id;
            InitParcel();
            DeleteParcelCommand = new(DeleteParcel, param => parcel.Error == null);
            OpenCustomerCommand = new(Tabs.OpenDetailes, null);
            OpenDroneCommand = new(Tabs.OpenDetailes, null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => parcel.Error == null);
            DelegateVM.ParcelChangedEvent += HandleAParcelChanged;

            if(parcel.AssignmentTime == null)
            {
                parcel.AssignmentTime = new DateTime();
            }
            if (parcel.CollectionTime == null)
            {
                parcel.CollectionTime = new DateTime();
            }
            if (parcel.DeliveryTime == null)
            {
                parcel.DeliveryTime = new DateTime();
            }
            //parcel.CollectionTime = DateTime.Now;
        }
        private void HandleAParcelChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id || e.Id == null)
                InitParcel();
        }
        public void InitParcel()
        {
            parcel = PLService.GetParcel(id);
        }
        public void DeleteParcel(object param)
        {
            if (MessageBox.Show("You're sure you want to delete this parcel?", "Delete Parcel", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                PLService.DeleteParcel(parcel.Id);
                MessageBox.Show("The parcel was successfully deleted");
                DelegateVM.ParcelChangedEvent -= HandleAParcelChanged;
                //DelegateVM.NotifyParcelChanged(parcel.Id);
                DelegateVM.NotifyParcelChanged();
                Tabs.CloseTab(param as TabItemFormat);
            }
        }

        public void parcelTreatedByDrone(object param)
        {
            try
            {
                if (parcel.AssignmentTime != null)
                {
                    if (parcel.CollectionTime != null)
                    {
                        PLService.DeliveryParcelByDrone(parcel.Drone.Id);
                        //DelegateVM.NotifyDroneChanged(parcel.Drone.Id);
                        DelegateVM.NotifyDroneChanged();
                    }
                    else
                    {
                        PLService.ParcelCollectionByDrone(parcel.Drone.Id);
                        //DelegateVM.NotifyDroneChanged(parcel.Drone.Id);
                        DelegateVM.NotifyDroneChanged();
                    }
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

    }
}
