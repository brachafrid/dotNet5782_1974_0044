using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using PL.PO;


namespace PL
{
    public class GenericList<T>
    {
        public RelayCommand ShowKindOfSortCommand { get; set; }
        public RelayCommand ValueTypeSortCommand { get; set; }
        public ListCollectionView list { set; get; }
        public ObservableCollection<string> SortOption { set; get; }
        public SortInputVM input { get; set; }
        
        public GenericList()
        {
            UpdateSortOptions();
            input = new(FilterNow);
            ShowKindOfSortCommand = new(ShowKindOfSort, null);
            ValueTypeSortCommand = new(input.ValueTypeSort, null);
        }

        public bool FilterD(object obj)
        {
            if (obj is DroneToList droneList)
            {
                if (input.ModelContain!= null && input.ModelContain != string.Empty)
                    return droneList.DroneModel.Contains(input.ModelContain);
            }
            return true;

        }
        public void FilterNow()
        {
            list.Filter += FilterD;
            list.IsLiveFiltering = true;
        }
        void UpdateSortOptions()
        {

            SortOption = new ObservableCollection<string>(typeof(T).GetProperties().Where(prop => prop.PropertyType.IsValueType || prop.PropertyType == typeof(string)).Select(prop => prop.Name).ToList());
        }

        public void ShowKindOfSort(object param)
        {
            input.SelectedKind = (param as ComboBox).SelectedValue.ToString();
            if (!typeof(T).GetProperty(input.SelectedKind).PropertyType.Name.Equals(typeof(string).Name))
            {
                input.VisibilityKindOfSort.visibility = Visibility.Visible;
            }
            else
            {
                input.StringSortVisibility.visibility = Visibility.Visible;
            }
        }
    }
}
