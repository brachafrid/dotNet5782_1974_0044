using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace PL.ViewModel
{
    public class GenericList<T>
    {
        public ListCollectionView list { set; get; }
        public List<string> SortOption { set; get; }
        private string selectedSort;
        public GenericList()
        {
            list.IsLiveFiltering = true;
            UpdateSortOptions();
        }
        void UpdateSortOptions()
        {
            SortOption = typeof(T).GetProperties().Where(type => type.PropertyType.IsValueType).Select(type => type.Name).ToList();
        }


    }
}
