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
    public class GenericList<T> : INotifyPropertyChanged
    {
        public List<string> KindOfSort { get; set; } = new() { "Range", "Single" };
        public Array WeightCategories { get; set; } = Enum.GetValues(typeof(WeightCategories));
        public Array PrioritiesArray { get; set; } = Enum.GetValues(typeof(Priorities));
        public Array DroneState { get; set; } = Enum.GetValues(typeof(DroneState));
        public Array PackageMode { get; set; } = Enum.GetValues(typeof(PackageModes));
        public ObservableCollection<string> SortOption { set; get; }
        public RelayCommand ShowKindOfSortCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public ListCollectionView list { set; get; }
        public Visble VisibilityKindOfSort { get; set; } = new();
        public Visble StringSortVisibility { get; set; } = new();
        public Visble VisibilityWeightCategories { get; set; } = new();
        public Visble VisibilityPriorities { get; set; } = new();
        public Visble VisibilityDroneState { get; set; } = new();
        public Visble VisbleDouble { set; get; } = new();
        public Visble VisblePackegeMode { set; get; } = new();
        public List<SortEntities> Filters { get; set; } = new();

        private double doubleFirstChange = 0;
        private double doubleLastChange = 100;
        public double DoubleFirstChange
        {
            get => doubleFirstChange;
            set
            {
                doubleFirstChange = value;
                SortEntities fiterWeight = Filters.FirstOrDefault(filter => filter.NameParameter == SelectedKind);
                if (fiterWeight == default)
                    Filters.Add(new SortEntities()
                    {
                        NameParameter = SelectedKind,
                        FirstParameter = doubleFirstChange
                    });
                else
                    Filters[Filters.IndexOf(fiterWeight)].FirstParameter = doubleFirstChange;
                FilterNow();
            }
        }
        public double DoubleLastChange
        {
            get => doubleLastChange;
            set
            {
                doubleLastChange = value;
                SortEntities fiterWeight = Filters.FirstOrDefault(filter => filter.NameParameter == SelectedKind);
                if (fiterWeight == default)
                    Filters.Add(new SortEntities()
                    {
                        NameParameter = SelectedKind,
                        FirstParameter = doubleLastChange
                    });
                else
                    Filters[Filters.IndexOf(fiterWeight)].FirstParameter = doubleLastChange;
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
                SortEntities fiterWeight = Filters.FirstOrDefault(filter => filter.NameParameter == SelectedKind);
                if (fiterWeight == default)
                    Filters.Add(new SortEntities()
                    {
                        NameParameter = SelectedKind,
                        Value = modelContain
                    });
                else
                    Filters[Filters.IndexOf(fiterWeight)].Value = modelContain;
                FilterNow();
                onPropertyChanged("ModelContain");
            }
        }
        public string SelectedKind { get; set; }
        public string selectedValue { get; set; }


        public GenericList()
        {
            UpdateSortOptions();
            ShowKindOfSortCommand = new(ShowKindOfSort, null);
            FilterCommand = new(FilterEnum, null);
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
            foreach (SortEntities item in Filters)
            {

                if (item.FirstParameter == null && item.LastParameter == null)
                {
                    if (obj.GetType().GetProperty(item.NameParameter).PropertyType.Name == "String")
                    {
                        if (!obj.GetType().GetProperty(item.NameParameter).GetValue(obj).ToString().Contains(item.Value))
                            return false;
                    }
                    else if (obj.GetType().GetProperty(item.NameParameter).GetValue(obj).ToString() != item.Value)
                        return false;
                }
                else if (obj.GetType().GetProperty(item.NameParameter).PropertyType.Name == "Double")
                {
                    if (item.LastParameter != null && (double)obj.GetType().GetProperty(item.NameParameter).GetValue(obj) > item.LastParameter)
                        return false;
                    if (item.FirstParameter != null && (double)obj.GetType().GetProperty(item.NameParameter).GetValue(obj) < item.FirstParameter)
                        return false;
                }
                else
                {
                    if (item.LastParameter != null && (int)obj.GetType().GetProperty(item.NameParameter).GetValue(obj) > item.LastParameter)
                        return false;
                    if (item.FirstParameter != null && (int)obj.GetType().GetProperty(item.NameParameter).GetValue(obj) < item.FirstParameter)
                        return false;
                }
            }
            return true;
        }
        public void ShowKindOfSort(object param)
        {
            SelectedKind = param.ToString();
            VisbleDouble.visibility = Visibility.Collapsed;
            VisibilityDroneState.visibility = Visibility.Collapsed;
            VisibilityPriorities.visibility = Visibility.Collapsed;
            VisibilityWeightCategories.visibility = Visibility.Collapsed;
            StringSortVisibility.visibility = Visibility.Collapsed;
            VisblePackegeMode.visibility = Visibility.Collapsed;
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
                case "PackageModes":
                    VisblePackegeMode.visibility = Visibility.Visible;
                    break;
                case "Int32":
                    VisbleDouble.visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public void FilterEnum(object param)
        {
            selectedValue = param.ToString();
            SortEntities fiterEnum = Filters.FirstOrDefault(filter => filter.NameParameter == SelectedKind);
            if (fiterEnum == default)
                Filters.Add(new SortEntities()
                {
                    NameParameter = SelectedKind,
                    Value = selectedValue
                });
            else
                Filters[Filters.IndexOf(fiterEnum)].Value = selectedValue;
            FilterNow();
        }       

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }


    }
}