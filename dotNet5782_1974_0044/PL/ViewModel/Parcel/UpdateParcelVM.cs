﻿using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PL
{
    class UpdateParcelVM : DependencyObject
    {
        private int id;
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
            init();
            DeleteParcelCommand = new(DeleteParcel, param => parcel.Error == null);

            DelegateVM.Parcel += init;
        }
        public void init()
        {
            parcel = PLService.GetParcel(id);
        }
        public void DeleteParcel(object param)
        {
            if (MessageBox.Show("You're sure you want to delete this parcel?", "Delete Parcel", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                PLService.DeleteParcel(parcel.Id);
                MessageBox.Show("The parcel was successfully deleted");
            }
        }

    }
}
