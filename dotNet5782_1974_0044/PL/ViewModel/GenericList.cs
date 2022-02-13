using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public abstract class GenericList<T> : NotifyPropertyChangedBase
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

        public ObservableCollection<T> sourceList;
        public ListCollectionView list
        {
            get => list1;
            set => Set(ref list1, value);
        }

        private Visibility visibilityKindOfSort = Visibility.Collapsed;

        public Visibility VisibilityKindOfSort
        {
            get { return visibilityKindOfSort; }
            set => Set(ref visibilityKindOfSort, value);
        }
        private Visibility stringSortVisibility = Visibility.Collapsed;

        public Visibility StringSortVisibility
        {
            get { return stringSortVisibility; }
            set => Set(ref stringSortVisibility, value);
        }
        private Visibility visibilityWeightCategories = Visibility.Collapsed;

        public Visibility VisibilityWeightCategories
        {
            get { return visibilityWeightCategories; }
            set => Set(ref visibilityWeightCategories, value);
        }

        private Visibility visibilityPriorities = Visibility.Collapsed;

        public Visibility VisibilityPriorities
        {
            get { return visibilityPriorities; }
            set => Set(ref visibilityPriorities, value);
        }
        private Visibility visibilityDroneState = Visibility.Collapsed;

        public Visibility VisibilityDroneState
        {
            get { return visibilityDroneState; }
            set => Set(ref visibilityDroneState, value);
        }
        private Visibility visbleDouble = Visibility.Collapsed;

        public Visibility VisbleDouble
        {
            get { return visbleDouble; }
            set => Set(ref visbleDouble, value);
        }
        private Visibility visblePackegeMode = Visibility.Collapsed;

        public Visibility VisblePackegeMode
        {
            get { return visblePackegeMode; }
            set => Set(ref visblePackegeMode, value);
        }
        private List<SortEntities> filters = new();

        public List<SortEntities> Filters
        {
            get { return filters; }
            set => Set(ref filters, value);
        }

        private int maxValue;

        public int MaxValue
        {
            get { return maxValue; }
            set => Set(ref maxValue, value);
        }


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
        ListCollectionView list1;

        public string ModelContain
        {
            get => modelContain;
            set
            {
                Set(ref modelContain, value);
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
            VisbleDouble = Visibility.Collapsed;
            VisibilityDroneState = Visibility.Collapsed;
            VisibilityPriorities = Visibility.Collapsed;
            VisibilityWeightCategories = Visibility.Collapsed;
            StringSortVisibility = Visibility.Collapsed;
            VisblePackegeMode = Visibility.Collapsed;
            ShowValueFilter(typeof(T).GetProperty(SelectedKind).PropertyType);
        }
        public void CancelFilter(object param)
        {
            Filters.RemoveAll((SortEntities o) => true);
            SelectedKind = string.Empty;
            VisbleDouble = Visibility.Collapsed;
            VisibilityDroneState = Visibility.Collapsed;
            VisibilityPriorities = Visibility.Collapsed;
            VisibilityWeightCategories = Visibility.Collapsed;
            StringSortVisibility = Visibility.Collapsed;
            VisblePackegeMode = Visibility.Collapsed;
            FilterNow();
        }
        public void ShowValueFilter(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case { } when typeof(string).Name == propertyType.Name:
                    StringSortVisibility = Visibility.Visible;
                    break;
                case { } when typeof(double).Name == propertyType.Name:
                    VisbleDouble = Visibility.Visible;
                    MaxValue = MaxValueFunc();
                    break;
                case { } when typeof(WeightCategories).Name == propertyType.Name:
                    VisibilityWeightCategories = Visibility.Visible;
                    break;
                case { } when typeof(Priorities).Name == propertyType.Name:
                    VisibilityPriorities = Visibility.Visible;
                    break;
                case { } when typeof(DroneState).Name == propertyType.Name:
                    VisibilityDroneState = Visibility.Visible;
                    break;
                case { } when typeof(PackageModes).Name == propertyType.Name:
                    VisblePackegeMode = Visibility.Visible;
                    break;
                case { } when typeof(int).Name == propertyType.Name:
                    VisbleDouble = Visibility.Visible;
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
    }

   
}