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
        public double doubleCange { get; set; }

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
        //public string selectedValue { get; set; }


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
                case "double":
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
                case "int":
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
            //static string selectedParameter = SelectedKind;
            string selectedValueParameter = selectedWeight;
                if (selectedValueParameter != null && selectedValueParameter != string.Empty)
                    return obj.GetType().GetProperties().FirstOrDefault(prop=>prop.PropertyType.Name== "WeightCategories").GetValue(obj).ToString() == selectedValueParameter;
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
            //static string selectedParameter = SelectedKind;
            string selectedValueParameter = selectedPiority;
                if (selectedValueParameter != null && selectedValueParameter != string.Empty)
                    return obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "Priorities").GetValue(obj).ToString() == selectedValueParameter;
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
            //static string selectedParameter = SelectedKind;
            string selectedValueParameter = selectedState;
                if (selectedValueParameter != null && selectedValueParameter != string.Empty)
                    return obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "DroneState").GetValue(obj).ToString() == selectedValueParameter;
            return true;

        }
        public bool FilterListModel(object obj)
        {
            //static string selectedParameter = SelectedKind;
            string selectedValueParameter = ModelContain;
                if (selectedValueParameter != null && selectedValueParameter != string.Empty)
                    return obj.GetType().GetProperties().FirstOrDefault(prop => prop.PropertyType.Name == "String").GetValue(obj).ToString().Contains(selectedValueParameter);
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
