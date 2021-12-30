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
        public RelayCommand ShowValueTypeSortCommand { get; set; }
        public RelayCommand FilterWheightommand { get; set; }
        public RelayCommand FilterPriorityCommand { get; set; }
        public RelayCommand FilterStateCommand { get; set; }
        public ListCollectionView list { set; get; }
        public ObservableCollection<string> SortOption { set; get; }
        public SortInputVM input { get; set; }

        public GenericList()
        {
            UpdateSortOptions();
            input = new(FilterNow);
            ShowKindOfSortCommand = new(ShowKindOfSort, null);
            ShowValueTypeSortCommand = new(ShowValueOfSort, null);
            FilterWheightommand = new(input.FilterWeightCategories, null);
            FilterPriorityCommand = new(input.FilterPriority, null);
            FilterStateCommand = new(input.FilterDroneState, null);
        }

        public bool FilterList(object obj)
        {
            if (obj is DroneToList droneList)
            {
                if (input.ModelContain != null && input.ModelContain != string.Empty)
                    return droneList.DroneModel.Contains(input.ModelContain);
                if (input.selectedWeight != null && input.selectedWeight != string.Empty)
                    return droneList.Weight.ToString() == input.selectedWeight;
                //if (input.selectedPiority != null && input.selectedPiority != string.Empty)
                //    return droneList.Weight.ToString() == input.selectedWeight;
                if (input.selectedState != null && input.selectedState != string.Empty)
                    return droneList.DroneState.ToString() == input.selectedState;
            }
            return true;

        }
        public void FilterNow()
        {
            list.Filter += FilterList;
            list.IsLiveFiltering = true;
        }
        void UpdateSortOptions()
        {
            SortOption = new ObservableCollection<string>(typeof(T).GetProperties().Where(prop => !prop.Name.Contains("Id") && (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))).Select(prop => prop.Name).ToList());
        }

        private void ShowValueOfSort(object param)
        {
            input.TypeOfSelectedParameter = typeof(T).GetProperty(input.SelectedKind).PropertyType;
            input.ShowValueOfSort(param);
        }
        public void ShowKindOfSort(object param)
        {
            input.SelectedKind = (param as ComboBox).SelectedValue.ToString();
            if (!typeof(T).GetProperty(input.SelectedKind).PropertyType.Name.Equals(typeof(string).Name))
            {
                input.VisibilityKindOfSort.visibility = Visibility.Visible;
                input.StringSortVisibility.visibility = Visibility.Collapsed;
                return;
            }
            input.StringSortVisibility.visibility = Visibility.Visible;
            input.VisibilityKindOfSort.visibility = Visibility.Collapsed;
        }


    }
}