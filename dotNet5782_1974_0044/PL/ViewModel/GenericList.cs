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
        public RelayCommand FilterWheightommand { get; set; }
        public RelayCommand FilterPriorityCommand { get; set; }
        public RelayCommand FilterStateCommand { get; set; }
        public ListCollectionView list { set; get; }
        public ObservableCollection<string> SortOption { set; get; }
        public SortInputVM input { get; set; }
        public List<Predicate<object>> Filters { get; set; } = new();
        

        public GenericList()
        {
            UpdateSortOptions();
            input = new(AddFilter, FilterNow, RemoveFilter);
            ShowKindOfSortCommand = new(ShowKindOfSort, null);
            FilterWheightommand = new(input.FilterWeightCategories, null);
            FilterPriorityCommand = new(input.FilterPriority, null);
            FilterStateCommand = new(input.FilterDroneState, null);

        }

        public void FilterNow()
        {
            list.Filter = InternalFilter;
            list.IsLiveFiltering = true;
        }
        public void AddFilter(Predicate<object> predicate)
        {
            Filters.Add(predicate);
        }
        public void RemoveFilter(Predicate<object> predicate)
        {
            Filters.Remove(predicate);
        }
        void UpdateSortOptions()
        {
            SortOption = new ObservableCollection<string>(typeof(T).GetProperties().Where(prop => !prop.Name.Contains("Id") && (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))).Select(prop => prop.Name).ToList());
        }
        public bool InternalFilter(object obj)
        {
            foreach (var item in Filters)
            {
                if (!item(obj))
                    return false;
            }
            return true;
        }
        public void ShowKindOfSort(object param)
        {
            input.SelectedKind = (param as ComboBox).SelectedValue.ToString();
            input.VisbleDouble.visibility = Visibility.Collapsed;
            input.VisibilityDroneState.visibility = Visibility.Collapsed;
            input.VisibilityPriorities.visibility = Visibility.Collapsed;
            input.VisibilityWeightCategories.visibility = Visibility.Collapsed;
            input.StringSortVisibility.visibility = Visibility.Collapsed;
            input.TypeOfSelectedParameter = typeof(T).GetProperty(input.SelectedKind).PropertyType;
            input.ShowValueFilter();
        }


    }
}