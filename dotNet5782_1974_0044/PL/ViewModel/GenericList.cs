using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace PL
{
    public class GenericList<T>
    {
        public ListCollectionView list { set; get; }
        public List<string> SortOption { set; get; }
        public string selectedSort { set; get; }
        public RelayCommand SortCommand { set; get; }
        public GenericList()
        {
            list.IsLiveFiltering = true;
            UpdateSortOptions();
        }
        void UpdateSortOptions()
        {
            SortOption = typeof(T).GetProperties().Where(prop => prop.PropertyType.IsEnum || prop.PropertyType == typeof(DateTime)).Select(prop => prop.Name).ToList();
        }
         void sort (object param)
        {

        }


    }
}
