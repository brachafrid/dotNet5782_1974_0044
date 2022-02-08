using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public abstract class GenericList<T> : DependencyObject, INotifyPropertyChanged
    {
        public List<string> KindOfSort { get; set; } = new() { "Range", "Single" };
        public Array WeightCategories { get; set; } = Enum.GetValues(typeof(WeightCategories));
        public Array PrioritiesArray { get; set; } = Enum.GetValues(typeof(Priorities));
        public Array DroneState { get; set; } = Enum.GetValues(typeof(DroneState));
        public Array PackageMode { get; set; } = Enum.GetValues(typeof(PackageModes));
        public ObservableCollection<string> SortOption { set; get; }
        public RelayCommand DoubleClick { set; get; }
        public RelayCommand ShowKindOfSortCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand CancelFilterCommand { get; set; }
        public RelayCommand CancelGroupCommand { get; set; }
        public RelayCommand GroupCommand { get; set; }
        public RelayCommand AddEntitiyCommand { get; set; }
        public ListCollectionView list { set; get; }
        public ObservableCollection<T> sourceList;
        public Visble VisibilityKindOfSort { get; set; } = new();
        public Visble StringSortVisibility { get; set; } = new();
        public Visble VisibilityWeightCategories { get; set; } = new();
        public Visble VisibilityPriorities { get; set; } = new();
        public Visble VisibilityDroneState { get; set; } = new();
        public Visble VisbleDouble { set; get; } = new();
        public Visble VisblePackegeMode { set; get; } = new();

        public List<SortEntities> Filters
        {
            get { return (List<SortEntities>)GetValue(FiltersProperty); }
            set { SetValue(FiltersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FiltersProperty =
            DependencyProperty.Register("Filters", typeof(List<SortEntities>), typeof(GenericList<T>), new PropertyMetadata(new List<SortEntities>()));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(GenericList<T>), new PropertyMetadata(0));


        private double doubleFirstChange = 0;
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
                        MinParameter = doubleFirstChange
                    });
                else
                    fiterWeight.MinParameter = doubleFirstChange;
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
        public string SelectedGroup { get; set; }
        public string selectedValue { get; set; }
        public GenericList()
        {
            UpdateSortOptions();
            ShowKindOfSortCommand = new(ShowKindOfSort, (object param) => param != null);
            FilterCommand = new(FilterEnum, null);
            CancelFilterCommand = new(CancelFilter, null);
            CancelGroupCommand = new(CancelGroup, null);
            GroupCommand = new(Grouping, null);
            AddEntitiyCommand = new(AddEntity, null);

        }
        public void FilterNow()
        {
            list.Filter = InternalFilter;
            list.IsLiveFiltering = true;
        }
        public abstract void AddEntity(object param);
        void UpdateSortOptions()
        {
            SortOption = new ObservableCollection<string>(typeof(T).GetProperties().Where(prop => !prop.Name.Contains("Id") && (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))).Select(prop => prop.Name));
        }
        public void Grouping(object param)
        {
            if (param != null)
            {
                SelectedGroup = param?.ToString();
                CancelGroup(param);
                list.GroupDescriptions.Add(new PropertyGroupDescription(SelectedGroup));
            }
        }
        public void CancelGroup(object param)
        {
            list.GroupDescriptions.Clear();
        }
        public bool InternalFilter(object obj)
        {
            foreach (SortEntities item in Filters)
            {
                if (obj.GetType().GetProperty(item.NameParameter).PropertyType.Name == typeof(string).Name)
                {
                    if (!obj.GetType().GetProperty(item.NameParameter).GetValue(obj).ToString().Contains(item.Value))
                        return false;
                }
                else if (obj.GetType().GetProperty(item.NameParameter).PropertyType.Name == typeof(double).Name)
                {
                    if ((double)obj.GetType().GetProperty(item.NameParameter).GetValue(obj) < item.MinParameter)
                        return false;
                }
                else if (obj.GetType().GetProperty(item.NameParameter).PropertyType.Name == typeof(int).Name)
                {
                    if ((int)obj.GetType().GetProperty(item.NameParameter).GetValue(obj) < item.MinParameter)
                        return false;
                }
                else if (obj.GetType().GetProperty(item.NameParameter).GetValue(obj).ToString() != item.Value)
                    return false;
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
        public void CancelFilter(object param)
        {
            Filters.RemoveAll((SortEntities o) => true);
            SelectedKind = string.Empty;
            VisbleDouble.visibility = Visibility.Collapsed;
            VisibilityDroneState.visibility = Visibility.Collapsed;
            VisibilityPriorities.visibility = Visibility.Collapsed;
            VisibilityWeightCategories.visibility = Visibility.Collapsed;
            StringSortVisibility.visibility = Visibility.Collapsed;
            VisblePackegeMode.visibility = Visibility.Collapsed;
            FilterNow();
        }
        public void ShowValueFilter(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case { } when typeof(string).Name == propertyType.Name:
                    StringSortVisibility.visibility = Visibility.Visible;
                    break;
                case { } when typeof(double).Name == propertyType.Name:
                    VisbleDouble.visibility = Visibility.Visible;
                    MaxValue = MaxValueFunc();
                    break;
                case { } when typeof(WeightCategories).Name == propertyType.Name:
                    VisibilityWeightCategories.visibility = Visibility.Visible;
                    break;
                case { } when typeof(Priorities).Name == propertyType.Name:
                    VisibilityPriorities.visibility = Visibility.Visible;
                    break;
                case { } when typeof(DroneState).Name == propertyType.Name:
                    VisibilityDroneState.visibility = Visibility.Visible;
                    break;
                case { } when typeof(PackageModes).Name == propertyType.Name:
                    VisblePackegeMode.visibility = Visibility.Visible;
                    break;
                case { } when typeof(int).Name == propertyType.Name:
                    VisbleDouble.visibility = Visibility.Visible;
                    MaxValue = MaxValueFunc();
                    break;
                default:
                    break;
            }
        }
        private int MaxValueFunc()
        {
            if (typeof(T) == typeof(DroneToList))
                return 100;
            return (int)sourceList.Max(itm => itm.GetType().GetProperty(SelectedKind).GetValue(itm));
        }
        public void FilterEnum(object param)
        {
            if (param != null)
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

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}