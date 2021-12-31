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
using System.ComponentModel;

namespace PL
{
    public class GenericList<T>: INotifyPropertyChanged
    {
        public RelayCommand ShowKindOfSortCommand { get; set; }
        public RelayCommand FilterWheightommand { get; set; }
        public RelayCommand FilterPriorityCommand { get; set; }
        public RelayCommand FilterStateCommand { get; set; }
        public ListCollectionView list { set; get; }
        public ObservableCollection<string> SortOption { set; get; }
        public List<Predicate<object>> Filters { get; set; } = new();
        public List<string> KindOfSort { get; set; } = new() { "Range", "Single" };
        public Array WeightCategories { get; set; } = Enum.GetValues(typeof(WeightCategories));
        public Array PrioritiesArray { get; set; } = Enum.GetValues(typeof(Priorities));
        public Array DroneState { get; set; } = Enum.GetValues(typeof(DroneState));
        public Visble VisibilityKindOfSort { get; set; } = new();
        public Visble StringSortVisibility { get; set; } = new();
        public Visble VisibilityWeightCategories { get; set; } = new();
        public Visble VisibilityPriorities { get; set; } = new();
        public Visble VisibilityDroneState { get; set; } = new();
        public Visble VisbleDouble { set; get; } = new();
        private double doubleFirstChange = 0;
        private double doubleLastChange = 100;
        public double DoubleFirstChange
        {
            get => doubleFirstChange;
            set
            {
                doubleFirstChange = value;
                 Filters.Remove(FilterListBattery);
                 Filters.Add(FilterListBattery);
                FilterNow();
            }
        }
        public double DoubleLastChange
        {
            get => doubleLastChange;
            set
            {
                doubleLastChange = value;
                 Filters.Remove(FilterListBattery);
                 Filters.Add(FilterListBattery);
                FilterNow();
            }
        }
        private string modelContain;
        public string ModelContain
        {
            get => modelContain;
            set
            {
                modelContain = value;
                 Filters.Remove(FilterListModel);
                 Filters.Add(FilterListModel);
                FilterNow();
                onPropertyChanged("ModelContain");
            }
        }
        public string SelectedKind { get; set; }
        public string selectedWeight { get; set; }
        public string selectedPiority { get; set; }
        public string selectedState { get; set; }


        public GenericList()
        {
            UpdateSortOptions();
            ShowKindOfSortCommand = new(ShowKindOfSort, null);
            FilterWheightommand = new(FilterWeightCategories, null);
            FilterPriorityCommand = new(FilterPriority, null);
            FilterStateCommand = new(FilterDroneState, null);

        }

        public void FilterNow()
        {
            list.Filter = InternalFilter;
            list.IsLiveFiltering = true;
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
            SelectedKind = (param as ComboBox).SelectedValue.ToString();
            VisbleDouble.visibility = Visibility.Collapsed;
            VisibilityDroneState.visibility = Visibility.Collapsed;
            VisibilityPriorities.visibility = Visibility.Collapsed;
            VisibilityWeightCategories.visibility = Visibility.Collapsed;
            StringSortVisibility.visibility = Visibility.Collapsed;
            ShowValueFilter(typeof(T).GetProperty(SelectedKind).PropertyType);
        }

         public void ShowValueFilter(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case "String":
                    StringSortVisibility.visibility = Visibility.Visible;
                    break;
                case "Double":
                    VisbleDouble.visibility = Visibility.Visible;
                    break;
                case "WeightCategories":
                    VisibilityWeightCategories.visibility = Visibility.Visible;
                    break;
                case "Priorities":
                    VisibilityPriorities.visibility = Visibility.Visible;
                    break;
                case "DroneState":
                    VisibilityDroneState.visibility = Visibility.Visible;
                    break;
                case "DateTime":
                    break;
                case "Int32":
                    VisbleDouble.visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public void FilterWeightCategories(object param)
        {
            selectedWeight = (param as ComboBox).SelectedValue.ToString();
             Filters.Remove(FilterListWeight);
             Filters.Add(FilterListWeight);
             FilterNow();
        }
        public bool FilterListWeight(object obj)
        {
            if (selectedWeight != null && selectedWeight != string.Empty)
                return obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "WeightCategories").GetValue(obj).ToString() == selectedWeight;
            return true;

        }
        public void FilterPriority(object param)
        {
            selectedPiority = (param as ComboBox).SelectedValue.ToString();
             Filters.Remove(FilterListPiority);
             Filters.Add(FilterListPiority);
             FilterNow();
        }
        public bool FilterListPiority(object obj)
        {
            if (selectedPiority != null && selectedPiority != string.Empty)
                return obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Priorities").GetValue(obj).ToString() == selectedPiority;
            return true;
        }
        public void FilterDroneState(object param)
        {
            selectedState = (param as ComboBox).SelectedValue.ToString();
             Filters.Remove(FilterListState);
             Filters.Add(FilterListState);
            FilterNow();
        }
        public bool FilterListState(object obj)
        {
            if (selectedState != null && selectedState != string.Empty)
                return obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "DroneState").GetValue(obj).ToString() == selectedState;
            return true;

        }
        public bool FilterListModel(object obj)
        {
            if (ModelContain != null && ModelContain != string.Empty)
                return obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "String").GetValue(obj).ToString().Contains(ModelContain);
            return true;
        }

        public bool FilterListBattery(object obj)
        {
            if (obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Double") != default)
                return (double)obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Double").GetValue(obj) > doubleFirstChange && (double)obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Double").GetValue(obj) < doubleLastChange;
            if (obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Int32" && !prop.Name.Contains("Id")) != default)
                return (int)obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Int32" && !prop.Name.Contains("Id")).GetValue(obj) > doubleFirstChange && (int)obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Int32" && !prop.Name.Contains("Id")).GetValue(obj) < doubleLastChange;
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }


    }
}