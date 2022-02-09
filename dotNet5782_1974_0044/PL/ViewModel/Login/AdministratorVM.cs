using PL.PO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace PL
{
    public class AdministratorVM: INotifyPropertyChanged
    {
        public RelayCommand AddDroneToListWindowCommand { get; set; }
        public RelayCommand AddParcelToListWindowCommand { get; set; }
        public RelayCommand AddStationToListWindowCommand { get; set; }
        public RelayCommand AddCustomerToListWindowCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
        private int selectedTab;

        public int SelectedTab
        {
            get { return selectedTab; }
            set { selectedTab = value;
                onPropertyChanged("SelectedTab");
            }
        }


        public AdministratorVM()
        {
            AddDroneToListWindowCommand = new(AddDroneToList, null);
            AddParcelToListWindowCommand = new(AddParcelToList, null);
            AddStationToListWindowCommand = new(AddStationToList, null);
            AddCustomerToListWindowCommand = new(AddCustomerToList, null);
            Tabs.changeSelectedTab += changeIndex;
        }


        public void AddDroneToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Drones",
                Content = new DroneToListVM()
            });
        }

        public void changeIndex(int index)
        {
            SelectedTab = index;
        }
        public void AddParcelToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcels",
                Content = new ParcelToListVM()
            });

        }
        public void AddStationToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Stations",
                Content = new StationToListVM()
            });

        }
        public void AddCustomerToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Customers",
                Content = new CustomerToListVM()
            });

        }
    }
}
