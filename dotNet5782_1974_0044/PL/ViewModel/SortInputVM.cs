using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using PL.PO;
namespace PL
{
    public class SortInputVM : DependencyObject, INotifyPropertyChanged
    {

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
        private double doubleFirstChange=0;
        private double doubleLastChange=100;
        public double DoubleFirstChange {
            get => doubleFirstChange;
            set {
                doubleFirstChange = value;
                RemoveFilter(FilterListBattery);
                AddFilter(FilterListBattery);
                FilterNow();
            }
        }
        public double DoubleLastChange
        {
            get => doubleLastChange;
            set
            {
                doubleLastChange = value;
                RemoveFilter(FilterListBattery);
                AddFilter(FilterListBattery);
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
                RemoveFilter(FilterListModel);
                AddFilter(FilterListModel);
                FilterNow();
                onPropertyChanged("ModelContain");
            }
        }
        public string SelectedKind { get; set; }
        public string selectedWeight { get; set; }
        public string selectedPiority { get; set; }
        public string selectedState { get; set; }
        public Type TypeOfSelectedParameter { get; set; }
        public Action<Predicate<object>> AddFilter;
        public Action<Predicate<object>> RemoveFilter;
        public Action FilterNow;
        public SortInputVM(Action<Predicate<object>> Filter, Action InternalFilter, Action<Predicate<object>> RemoveAFilter)
        {
            AddFilter = Filter;
            FilterNow = InternalFilter;
            RemoveFilter = RemoveAFilter;
        }


        public void ShowValueFilter()
        {
            switch (TypeOfSelectedParameter.Name)
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
                case "Int":
                    break;
                default:
                    break;
            }
        }

        public void FilterWeightCategories(object param)
        {
            selectedWeight = (param as ComboBox).SelectedValue.ToString();
            RemoveFilter(FilterListWeight);
            AddFilter(FilterListWeight);
            FilterNow();
        }
        public bool FilterListWeight(object obj)
        {
                if (selectedWeight != null && selectedWeight != string.Empty)
                    return obj.GetType().GetProperties().FirstOrDefault(prop=>prop.PropertyType.Name== "WeightCategories").GetValue(obj).ToString() == selectedWeight;
            return true;

        }
        public void FilterPriority(object param)
        {
            selectedPiority = (param as ComboBox).SelectedValue.ToString();
            RemoveFilter(FilterListPiority);
            AddFilter(FilterListPiority);
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
            RemoveFilter(FilterListState);
            AddFilter(FilterListState);
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
            return (double)obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Double").GetValue(obj)>doubleFirstChange && (double)obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Double").GetValue(obj) < doubleLastChange;    
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}
