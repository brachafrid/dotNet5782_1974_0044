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
        /// <summary>
        /// List of kind of sorts
        /// </summary>
        public List<string> KindOfSort { get; set; } = new() { "Range", "Single" };
        /// <summary>
        /// Array of weight categories
        /// </summary>
        public Array WeightCategories { get; set; } = Enum.GetValues(typeof(WeightCategories));
        /// <summary>
        /// Array of priorities
        /// </summary>
        public Array PrioritiesArray { get; set; } = Enum.GetValues(typeof(Priorities));
        /// <summary>
        /// Array of drone states
        /// </summary>
        public Array DroneState { get; set; } = Enum.GetValues(typeof(DroneState));
        /// <summary>
        /// Array of parcel modes
        /// </summary>
        public Array PackageMode { get; set; } = Enum.GetValues(typeof(PackageModes));
        /// <summary>
        /// ObservableCollection of sort option
        /// </summary>
        public ObservableCollection<string> SortOption { set; get; }
        /// <summary>
        /// Command of double click
        /// </summary>
        public RelayCommand DoubleClick { set; get; }
        /// <summary>
        /// Command of showing kind of sort
        /// </summary>
        public RelayCommand ShowKindOfSortCommand { get; set; }
        /// <summary>
        /// Command of filtering
        /// </summary>
        public RelayCommand FilterCommand { get; set; }
        /// <summary>
        /// Command of canceling filter
        /// </summary>
        public RelayCommand CancelFilterCommand { get; set; }
        /// <summary>
        /// Command of canceling group
        /// </summary>
        public RelayCommand CancelGroupCommand { get; set; }
        /// <summary>
        /// Command of grouping
        /// </summary>
        public RelayCommand GroupCommand { get; set; }
        /// <summary>
        /// Command of adding entiti
        /// </summary>
        public RelayCommand AddEntitiyCommand { get; set; }

        public ObservableCollection<T> sourceList;
        /// <summary>
        /// Generic ListCollectionView
        /// </summary>
        public ListCollectionView list
        {
            get => list1;
            set => Set(ref list1, value);
        }
        
        private FilterType? filterType;
        /// The type of filter now
        /// </summary>
        public FilterType? FilterType
        {
            get { return filterType; }
            set { Set(ref filterType, value); }
        }


        private List<SortEntities> filters = new();

        /// <summary>
        /// List of filters
        /// </summary>
        public List<SortEntities> Filters
        {
            get { return filters; }
            set => Set(ref filters, value);
        }

        private int maxValue;

        /// <summary>
        /// The max value
        /// </summary>
        public int MaxValue
        {
            get { return maxValue; }
            set => Set(ref maxValue, value);
        }

        private double doubleFirstChange = 0;
        /// <summary>
        /// Min parameter
        /// </summary>
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

        /// <summary>
        /// Model contain
        /// </summary>
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
        /// <summary>
        /// The selected kind
        /// </summary>
        public string SelectedKind { get; set; }
        /// <summary>
        /// The selected group
        /// </summary>
        public string SelectedGroup { get; set; }
        /// <summary>
        /// The selected value
        /// </summary>
        public string selectedValue { get; set; }

        /// <summary>
        /// constructor GenericList
        /// </summary>
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

        /// <summary>
        /// Filter now
        /// </summary>
        public void FilterNow()
        {
            list.Filter = InternalFilter;
            list.IsLiveFiltering = true;
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="param"></param>
        public abstract void AddEntity(object param);

        /// <summary>
        /// Update sort options
        /// </summary>
        void UpdateSortOptions()
        {
            SortOption = new ObservableCollection<string>(typeof(T).GetProperties().Where(prop => !prop.Name.Contains("Id") && (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))).Select(prop => prop.Name));
        }

        /// <summary>
        /// Grouping
        /// </summary>
        /// <param name="param"></param>
        public void Grouping(object param)
        {
            if (param != null)
            {
                SelectedGroup = param?.ToString();
                CancelGroup(param);
                list.GroupDescriptions.Add(new PropertyGroupDescription(SelectedGroup));
            }
        }

        /// <summary>
        /// Cancels  group
        /// </summary>
        /// <param name="param"></param>
        public void CancelGroup(object param)
        {
            list.GroupDescriptions.Clear();
        }

        /// <summary>
        /// Internal filter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>If it meets the conditions</returns>
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

        /// <summary>
        /// Show kind of sort
        /// </summary>
        /// <param name="param"></param>
        public void ShowKindOfSort(object param)
        {
            SelectedKind = param.ToString();
            FilterType = null;
            ShowValueFilter(typeof(T).GetProperty(SelectedKind).PropertyType);
        }

        /// <summary>
        /// Cancels filter
        /// </summary>
        /// <param name="param"></param>
        public void CancelFilter(object param)
        {
            Filters.RemoveAll((SortEntities o) => true);
            SelectedKind = string.Empty;
            FilterType = null;
            FilterNow();
        }

        /// <summary>
        /// Show value filter
        /// </summary>
        /// <param name="propertyType"></param>
        public void ShowValueFilter(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case { } when typeof(string).Name == propertyType.Name:
                    FilterType = PO.FilterType.STRING;
                    break;
                case { } when typeof(double).Name == propertyType.Name:
                    FilterType = PO.FilterType.NUMBER;
                    MaxValue = MaxValueFunc();
                    break;
                case { } when typeof(WeightCategories).Name == propertyType.Name:
                    FilterType = PO.FilterType.WEGHIT;
                    break;
                case { } when typeof(Priorities).Name == propertyType.Name:
                    FilterType = PO.FilterType.PIORITY;
                    break;
                case { } when typeof(DroneState).Name == propertyType.Name:
                    FilterType = PO.FilterType.STATE;
                    break;
                case { } when typeof(PackageModes).Name == propertyType.Name:
                    FilterType = PO.FilterType.PACKEGE;
                    break;
                case { } when typeof(int).Name == propertyType.Name:
                    FilterType = PO.FilterType.NUMBER;
                    MaxValue = MaxValueFunc();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Max value function
        /// </summary>
        /// <returns>Max value</returns>
        private int MaxValueFunc()
        {
            if (typeof(T) == typeof(DroneToList))
                return 100;
            return (int)sourceList.Max(itm => itm.GetType().GetProperty(SelectedKind).GetValue(itm));
        }

        /// <summary>
        /// Filter Enum
        /// </summary>
        /// <param name="param"></param>
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