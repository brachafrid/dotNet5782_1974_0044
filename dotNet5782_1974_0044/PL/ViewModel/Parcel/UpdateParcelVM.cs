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
    class UpdateParcelVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
        private readonly int id;
        public RelayCommand OpenCustomerCommand { get; set; }
        public RelayCommand OpenDroneCommand { get; set; }
        public RelayCommand ParcelTreatedByDrone { get; set; }

        private Parcel parcel;

        public Parcel Parcel
        {
            get { return parcel; }
            set { parcel = value;
                onPropertyChanged("Parcel");
            }
        }

        public RelayCommand DeleteParcelCommand { get; set; }

        public UpdateParcelVM(int id)
        {
            this.id = id;
            InitParcel();
            DeleteParcelCommand = new(DeleteParcel, param => Parcel.Error == null);
            OpenCustomerCommand = new(Tabs.OpenDetailes, null);
            OpenDroneCommand = new(Tabs.OpenDetailes, null);
            ParcelTreatedByDrone = new(parcelTreatedByDrone, param => Parcel.Error == null);
            DelegateVM.ParcelChangedEvent += HandleAParcelChanged;

            if(Parcel.AssignmentTime == null)
            {
                Parcel.AssignmentTime = new DateTime();
            }
            if (Parcel.CollectionTime == null)
            {
                Parcel.CollectionTime = new DateTime();
            }
            if (Parcel.DeliveryTime == null)
            {
                Parcel.DeliveryTime = new DateTime();
            }
            //Parcel.CollectionTime = DateTime.Now;
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
                PLService.DeleteParcel(Parcel.Id);
                MessageBox.Show("The parcel was successfully deleted");
                DelegateVM.ParcelChangedEvent -= HandleAParcelChanged;
                //DelegateVM.NotifyParcelChanged(Parcel.Id);
                DelegateVM.NotifyParcelChanged();
                Tabs.CloseTab(param as TabItemFormat);
            }
        }

        public void parcelTreatedByDrone(object param)
        {
            try
            {
                if (Parcel.AssignmentTime != null)
                {
                    if (Parcel.CollectionTime != null)
                    {
                        PLService.DeliveryParcelByDrone(Parcel.Drone.Id);
                        //DelegateVM.NotifyDroneChanged(Parcel.Drone.Id);
                        DelegateVM.NotifyDroneChanged();
                    }
                    else
                    {
                        PLService.ParcelCollectionByDrone(Parcel.Drone.Id);
                        //DelegateVM.NotifyDroneChanged(Parcel.Drone.Id);
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
