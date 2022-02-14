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

        /// <summary>
        /// On property changed
        /// </summary>
        /// <param name="properyName"></param>
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

        /// <summary>
        /// constructor
        /// </summary>
        public AdministratorVM()
        {
            AddDroneToListWindowCommand = new(AddDroneToList, null);
            AddParcelToListWindowCommand = new(AddParcelToList, null);
            AddStationToListWindowCommand = new(AddStationToList, null);
            AddCustomerToListWindowCommand = new(AddCustomerToList, null);
            Tabs.changeSelectedTab += changeIndex;
        }

        /// <summary>
        /// Add tab of drone to list
        /// </summary>
        /// <param name="param"></param>
        public void AddDroneToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Drones",
                Content = new DroneToListVM()
            });
        }

        /// <summary>
        /// change index
        /// </summary>
        /// <param name="index"></param>
        public void changeIndex(int index)
        {
            SelectedTab = index;
        }

        /// <summary>
        /// Add tab of parcel to list
        /// </summary>
        /// <param name="param"></param>
        public void AddParcelToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Parcels",
                Content = new ParcelToListVM()
            });

        }

        /// <summary>
        /// Add tab of station to list
        /// </summary>
        /// <param name="param"></param>
        public void AddStationToList(object param)
        {
            Tabs.AddTab(new TabItemFormat()
            {
                Header = "Stations",
                Content = new StationToListVM()
            });

        }

        /// <summary>
        /// Add tab of customer to list
        /// </summary>
        /// <param name="param"></param>
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
