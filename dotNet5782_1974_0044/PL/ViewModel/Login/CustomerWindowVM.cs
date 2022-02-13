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
        private int selectedTab;

        public int SelectedTab
        {
            get { return selectedTab; }
            set { selectedTab = value;
                onPropertyChanged("SelectedTab");
            }
        }

        private Customer customer=new();

        public Customer Customer
        {
            get { return customer; }
            set { customer = value;
                onPropertyChanged("Customer");
            }
        }
        private Visibility visibilityCustomer = Visibility.Collapsed;

        public Visibility VisibilityCustomer
        {
            get { return visibilityCustomer; }
            set { visibilityCustomer = value;
                onPropertyChanged("VisibilityCustomer");
            }
        }
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
        public CustomerWindowVM( int Id)    
        {
            id = Id;
            Init();
            Tabs.changeSelectedTab += changeIndex;
            AddParcelCommand = new(AddParcel, null);
            DisplayParcelsFromCommand = new(DisplayParcelsFrom, null);
            DisplayParcelsToCommand = new(DisplayParcelsTo, null);
            DisplayCustomerCommand = new(DisplayCustomer, null);
            DelegateVM.CustomerChangedEvent += HandleCustomerChanged;
            DelegateVM.ParcelChangedEvent +=(sender,e)=> Init();
        }
        private void HandleCustomerChanged(object sender, EntityChangedEventArgs e)
        {
            if (id == e.Id||e.Id==null)
                Init();
        }
        public async void Init()
        {
            customer = await PLService.GetCustomer(id); 
        }

        public void changeIndex(int index)
        {
            SelectedTab = index;
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
