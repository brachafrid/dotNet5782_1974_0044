using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL
{
    public class AddParcelVM : NotifyPropertyChangedBase, IDisposable
    {
        public ParcelAdd parcel { set; get; }
        public RelayCommand AddParcelCommand { get; set; }
        public ObservableCollection<int> customers { get; set; }
        public Array piorities { get; set; }
        public Array Weight { get; set; }
        public bool IsAdministor { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="isAdministor">is administor</param>
        /// <param name="id">id</param>
        public AddParcelVM(bool isAdministor, int id = 0)
        {
            parcel = new();
            InitCustomersList();
            AddParcelCommand = new(Add, param => parcel.Error == null);
            piorities = Enum.GetValues(typeof(Priorities));
            Weight = Enum.GetValues(typeof(WeightCategories));
            IsAdministor = isAdministor;
            DelegateVM.ParcelChangedEvent += HandleCustomerListChanged;
            if (!isAdministor)
                parcel.CustomerSender = id;
        }

        /// <summary>
        /// Initializes the customers list
        /// </summary>
        private async void InitCustomersList()
        {
            try
            {
                customers = new ObservableCollection<int>((await PLService.GetCustomers())
                    .Select(customer => customer.Id));
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
        private async void HandleCustomerListChanged(object sender, EntityChangedEventArgs e)
        {

            customers.Clear();
            foreach (var item in (await PLService.GetCustomers()).Select(customer => customer.Id))
                customers.Add(item);

        }


        /// <summary>
        /// Add parcel
        /// </summary>
        /// <param name="param"></param>
        public async void Add(object param)
        {
            try
            {
                await PLService.AddParcel(parcel);
                DelegateVM.NotifyParcelChanged();
                if (IsAdministor)
                {
                    DelegateVM.NotifyCustomerChanged(parcel.CustomerReceives);
                    DelegateVM.NotifyCustomerChanged(parcel.CustomerSender);
                }
                Tabs.CloseTab(param as TabItemFormat);
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("sender or reciver not exist");
            }
            catch (BO.DeletedExeption ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
            catch (BO.XMLFileLoadCreateException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }
        public void Dispose()
        {
        }
    }
}
