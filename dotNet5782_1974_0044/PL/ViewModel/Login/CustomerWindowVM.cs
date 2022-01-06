using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public partial class CustomerWindowVM : DependencyObject
    {

        public List<ParcelToList> list { set; get; } 
        public Visble VisibilityParcelsList { get; set; } = new();

        public Customer customer
        {
            get { return (Customer)GetValue(customerProperty); }
            set { SetValue(customerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerProperty =
            DependencyProperty.Register("customer", typeof(Customer), typeof(CustomerWindowVM), new PropertyMetadata(new Customer()));

        //List<ParcelToList> listParcels;
        public RelayCommand DisplayParcelsCommand { get; set; }

        public CustomerWindowVM()
        {
            Init();
            DisplayParcelsCommand = new(DisplayParcels, param => customer.Error == null);
            list = new List<ParcelToList>();
                //{ new Parcel() { Id = 77, Piority = Priorities.FAST, Weight = WeightCategories.HEAVY },
            //new Parcel() { Id = 77, Piority = Priorities.FAST, Weight = WeightCategories.HEAVY }};// ListCollectionView();
            //listParcels = new List<ParcelToList>();
            DelegateVM.Customer += Init;
        }

        public void Init()
        {
           customer = new CustomerHandler().GetCustomer(2);
        }

        public void DisplayParcels(object param)
        {
            list = new ParcelHandler().GetParcels().ToList();
            VisibilityParcelsList.visibility = Visibility.Visible;
            //public ListCollectionView list { set; get; }
            MessageBox.Show($"{list}");
            //listParcels = ParcelHandler().GetParcels;
        }
}
}
