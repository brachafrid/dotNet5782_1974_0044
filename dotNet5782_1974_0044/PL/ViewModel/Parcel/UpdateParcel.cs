using PL.PO;
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
        public Parcel parcel
        {
            get { return (Parcel)GetValue(parcelProperty); }
            set { SetValue(parcelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty parcelProperty =
            DependencyProperty.Register("parcel", typeof(Parcel), typeof(UpdateParcelVM), new PropertyMetadata(new Parcel()));

        public RelayCommand DeleteParcelCommand { get; set; }

        public UpdateParcelVM()
        {
            init();
            DeleteParcelCommand = new(DeleteParcel, param => parcel.Error == null);

            DelegateVM.Parcel += init;
        }
        public void init()
        {
            parcel = new ParcelHandler().GetParcel(2);
        }
        public void DeleteParcel(object param)
        {
            new ParcelHandler().DeleteParcel(parcel.Id);
        }

    }
}
