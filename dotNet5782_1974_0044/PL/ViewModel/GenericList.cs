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
    public class GenericList<T> : SortInputVM
    {
        public ListCollectionView list { set; get; }
        public ObservableCollection<string> SortOption { set; get; }
        public string selectedSort { set; get; }
        public RelayCommand SortCommand { set; get; }
        public SortInputVM m;

        public GenericList()
        {
            //list.Filter +=;
            //list.IsLiveFiltering = true;
            SortCommand = new(option, null);
            UpdateSortOptions();
            m = new();
            m.te ="lll";
        }
        void UpdateSortOptions()
        {
            SortOption = new ObservableCollection<string>(typeof(T).GetProperties().Where(prop => prop.PropertyType.IsEnum).Select(prop => prop.Name).ToList());
        }
            void option(object param)
        {
            OptionVisibility = Visibility.Visible;
        }




    }
}
