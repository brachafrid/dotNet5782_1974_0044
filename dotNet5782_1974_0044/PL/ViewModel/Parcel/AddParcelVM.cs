using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PL.PO;

namespace PL
{
   public class AddParcelVM: INotifyPropertyChanged
    {
        public ParcelAdd parcel { set; get; }
        public RelayCommand AddParcelCommand { get; set; }
        public RelayCommand VisibilityParcel { get; set; }
        public RelayCommand VisibilitySender { get; set; }
        private Visibility visibleParcel;

        public Visibility VisibleParcel
        {
            get { return visibleParcel; }
            set { visibleParcel = value;
            }
        }
        private Visibility visibleSender;

        public Visibility VisibleSender
        {
            get { return visibleSender; }
            set
            {
                visibleSender = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
        public IEnumerable<int> customers { get; set; }
        public Array piorities { get; set; }
        public Array Weight { get; set; }
        public bool IsAdministor { get; set; }
        public AddParcelVM(bool isAdministor, int id= 0)
        {
            parcel = new( );
            InitCustomersList();
            AddParcelCommand = new(Add, param => parcel.Error == null);
            VisibilityParcel = new(visibilityParcel, param => parcel.Error == null);
            piorities = Enum.GetValues(typeof(Priorities));
            Weight = Enum.GetValues(typeof(WeightCategories));
            IsAdministor = isAdministor;
           if (!isAdministor)
                parcel.CustomerSender = id;
        }

        private async void InitCustomersList()
        {
            customers = (await PLService.GetCustomers())
                .Select(customer => customer.Id);
        }

        public void visibilityParcel(object param)
        {
            VisibleParcel = Visibility.Visible;
        }
        public async void Add(object param)
        {
            try
            {
              await  PLService.AddParcel(parcel);
                DelegateVM.NotifyParcelChanged();
                //DelegateVM.NotifyCustomerChanged(parcel.CustomerReceives);
                DelegateVM.NotifyCustomerChanged();
                //DelegateVM.NotifyCustomerChanged(parcel.CustomerSender);
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch(KeyNotFoundException)
            {
                MessageBox.Show("sender or reciver not exist");
            }
        }
    }
}
