using PL.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;


namespace PL
{
    public partial class CustomerWindowVM : GenericList<ParcelAtCustomer>
    {


        //public List<ParcelAtCustomer> listParcels
        //{
        //    get { return (List<ParcelAtCustomer>)GetValue(listParcelsProperty); }
        //    set { SetValue(listParcelsProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for listParcels.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty listParcelsProperty =
        //    DependencyProperty.Register("listParcels", typeof(List<ParcelAtCustomer>), typeof(CustomerWindowVM), new PropertyMetadata(new List<ParcelAtCustomer>()));


        public Visble VisibilityCustomer { get; set; } = new();

        public Customer customer = new Customer();
        public RelayCommand DisplayParcelsCommand { get; set; }
        public RelayCommand sendParcel { get; set; }
        public RelayCommand collectionParcel { get; set; }
        public RelayCommand gettingParcel { get; set; }
        //public RelayCommand ToCustomerCommand { get; set; }
        //public RelayCommand FromCustomerCommand { get; set; }


        public ParcelAdd parcel { set; get; }

        public CustomerWindowVM()
        {
            Init();
            customer = new CustomerHandler().GetCustomer(2);
            DisplayParcelsCommand = new(DisplayParcels, null);
            //ToCustomerCommand = new(ToCustomer, null);
            //FromCustomerCommand = new(FromCustomer, null);
            sendParcel = new(SendParcel, null);
            collectionParcel = new(CollectionParcel, null);
            gettingParcel = new(GettingParcel, null);
            //list = new ListCollectionView(null);
            list = new ListCollectionView(new ParcelHandler().GetParcels().ToList());
            List<ParcelAtCustomer> fromCustomer = customer.FromCustomer;
            List<ParcelAtCustomer> toCustomer = customer.ToCustomer;
            //list = new ListCollectionView(toCustomer.ToList());
            list = new ListCollectionView(fromCustomer.ToList());
            DelegateVM.Customer += Init;
            DelegateVM.Parcel += Init;
        }
        public void Init()
        {
            customer = new CustomerHandler().GetCustomer(2);
        }
        public void DisplayParcels(object param)
        {
            customer = new CustomerHandler().GetCustomer(2);

            MessageBox.Show($"{customer.Name}");
            MessageBox.Show($"{customer.FromCustomer[0].Customer}");
            VisibilityCustomer.visibility = Visibility.Visible;
            //DelegateVM.Customer();
        }
        //public void ToCustomer(object param)
        //{
        //    List<ParcelAtCustomer> toCustomer = customer.ToCustomer;
        //    list = new ListCollectionView(toCustomer.ToList());
        //    VisibilityCustomer.visibility = Visibility.Visible;
        //}
        //public void FromCustomer(object param)
        //{ 
        //    List<ParcelAtCustomer> fromCustomer = customer.FromCustomer;
        //    list = new ListCollectionView(fromCustomer.ToList());
        //    VisibilityCustomer.visibility = Visibility.Visible;
        //}
        public void SendParcel(object param)
        {
           new  AddParcelVM(customer.Id);
            //parcel = new(){ CustomerSender = customer.Id, CustomerReceives = 0, Piority = Priorities.REGULAR, Weight = PO.WeightCategories.MEDIUM };
            //new ParcelHandler().AddParcel(parcel);
            //customer.FromCustomer.Add(new ParcelHandler().ConvertParcelAddToParcelAtCustomer(parcel));
            DelegateVM.Customer();
            DelegateVM.Parcel();
        }

        public void CollectionParcel(object param)
        { 
        
        }

        public void GettingParcel(object param)
        {

        }
    }
}
