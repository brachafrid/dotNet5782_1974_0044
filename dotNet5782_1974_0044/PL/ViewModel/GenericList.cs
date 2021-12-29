using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Collections.ObjectModel;

namespace PL
{
    public class GenericList<T> 
    {
        public ListCollectionView list { set; get; }
        public ObservableCollection<string> SortOption { set; get; }
        public SortInputVM input { get; set; }

        public GenericList()
        {
            UpdateSortOptions();
            input = new();
        }
        void UpdateSortOptions()
        {
            SortOption = new ObservableCollection<string>(typeof(T).GetProperties().Where(prop => prop.PropertyType.IsValueType||prop.PropertyType == typeof(string)).Select(prop => prop.Name).ToList());
        }

    }
}
