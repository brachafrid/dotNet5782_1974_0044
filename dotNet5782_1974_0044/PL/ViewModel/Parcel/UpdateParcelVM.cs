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
        private int id;
        public RelayCommand OpenCustomerCommand { get; set; }
        public RelayCommand OpenDroneCommand { get; set; }
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
            OpenCustomerCommand = new(OpenCustomerDetails, null);
            OpenDroneCommand = new(OpenDroneDetails, null);
            DelegateVM.Parcel += InitParcel;
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
                Tabs.CloseTab((param as TabItemFormat).Text);
                DelegateVM.Parcel -= InitParcel;
                DelegateVM.Parcel?.Invoke();
            }
        }
        public void OpenCustomerDetails(object param)
        {
            if (param != null && param is int Id)
                Tabs.AddTab(new()
                {
                    TabContent = "UpdateCustomerView",
                    Text = "customer " + Id,
                    Id = Id
                });
        }

        public void OpenDroneDetails(object param)
        {
            if (param != null && param is int Id)
                Tabs.AddTab(new()
                {
                    TabContent = "UpdateDroneView",
                    Text = "drone " + Id,
                    Id = Id
                });
        }

    }
}
