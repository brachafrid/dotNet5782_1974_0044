using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PL.PO;

namespace PL
{
   public class AddParcelVM
    {
        public ParcelAdd parcel { set; get; }
        public RelayCommand AddParcelCommand { get; set; }
        public RelayCommand VisibilityParcel { get; set; }
        public RelayCommand VisibilitySender { get; set; }
        public Visble VisibleParcel { get; set; }
        public Visble VisibleSender { get; set; }
        public IEnumerable<int> customers { get; set; }
        public Array piorities { get; set; }
        public Array Weight { get; set; }
        public bool IsAdministor { get; set; }
        public AddParcelVM(bool isAdministor, int id= 0)
        {
            parcel = new( );
            customers = PLService.GetCustomers().Select(customer => customer.Id);
            AddParcelCommand = new(Add, param => parcel.Error == null);
            VisibilityParcel = new(visibilityParcel, param => parcel.Error == null);
            piorities = Enum.GetValues(typeof(Priorities));
            Weight = Enum.GetValues(typeof(WeightCategories));
            IsAdministor = isAdministor;
           if (!isAdministor)
                parcel.CustomerSender = id;
        }
        public void visibilityParcel(object param)
        {
            VisibleParcel.visibility = Visibility.Visible;
        }
        public void Add(object param)
        {
            try
            {
                PLService.AddParcel(parcel);
                DelegateVM.Parcel?.Invoke();
                DelegateVM.Customer?.Invoke();
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch(KeyNotFoundException)
            {
                MessageBox.Show("sender or reciver not exist");
            }
        }
    }
}
