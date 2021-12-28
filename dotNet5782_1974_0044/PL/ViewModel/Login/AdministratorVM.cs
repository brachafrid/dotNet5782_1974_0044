using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL.PO;

namespace PL
{
    public class AdministratorVM
    {
        public ObservableCollection<TabItemFormat> TabItems { get; set; } = new();
        public RelayCommand AddDroneToListWindow { get; set; }
        public RelayCommand AddParcelToListWindow { get; set; }
        public RelayCommand AddStationToListWindow { get; set; }
        public RelayCommand AddCustomerToListWindow { get; set; }
        public AdministratorVM()
        {
        }
    }
}
