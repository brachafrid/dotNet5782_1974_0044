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
        public static IntDependency SelectedTab { get; set; } = new();
        public Customer customer
        {
            get { return (Customer)GetValue(customerProperty); }
            set { SetValue(customerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for customer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty customerProperty =
            DependencyProperty.Register("customer", typeof(Customer), typeof(CustomerWindowVM), new PropertyMetadata(new Customer()));
        public Visble VisibilityCustomer { get; set; } = new();

        //public Customer customer = new Customer();
        public RelayCommand DisplayParcelsCommand { get; set; }
        public RelayCommand sendParcel { get; set; }
        public RelayCommand collectionParcel { get; set; }
        public RelayCommand gettingParcel { get; set; }
        public RelayCommand AddParcelCommand { get; set; }
        public RelayCommand DisplayParcelsFromCommand { get; set; }
        public RelayCommand DisplayParcelsToCommand { get; set; }
        public RelayCommand DisplayCustomerCommand { get; set; }
        public ParcelAdd parcel { set; get; }
        int id;
        public CustomerWindowVM(int Id)
        {
            id = Id;
            Init();
            Tabs.changeSelectedTab += changeIndex;
            AddParcelCommand = new(AddParcel, null);
            DisplayParcelsFromCommand = new(DisplayParcelsFrom, null);
            DisplayParcelsToCommand = new(DisplayParcelsTo, null);
            DisplayCustomerCommand = new(DisplayCustomer, null);
            DelegateVM.Customer += Init;
            DelegateVM.Parcel += Init;
        }

        public void Init()
        {
            customer = PLService.GetCustomer(id);
        }

        public void changeIndex(int index)
        {
            SelectedTab.Instance = index;
        }
        public void DisplayCustomer(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            { 
                Header = "Customer",
                Content = new UpdateCustomerVM(id,false),
            });
        }
        public void AddParcel(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Add Parcel",
                Content = new AddParcelVM(false,id),
            });
        }
        public void DisplayParcelsFrom(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            { 
                Header = "Parcels From Customer",
                Content = new ParcelToListVM(id, "From"),
            });
        }
        public void DisplayParcelsTo(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcels To Customer",
                Content = new ParcelToListVM(id, "To"),
            });
        }

        public override void AddEntity(object param)
        {
            throw new NotImplementedException();
        }
    }
}
