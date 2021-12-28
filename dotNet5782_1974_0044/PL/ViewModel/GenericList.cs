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
    public class GenericList<T>:DependencyObject
    {
        public ListCollectionView list { set; get; }
        public List<string> SortOption { set; get; }
        public string selectedSort { set; get; }
        public RelayCommand SortCommand { set; get; }
        public Visibility DateTimeOptionsVisbility
        {
            get { return (Visibility)GetValue(DateTimeVisbilityProperty); }
            set { SetValue(DateTimeVisbilityProperty, value); }
        }
        // Using a DependencyProperty as the backing store for DateTimeOptionsVisbility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeVisbilityProperty =
            DependencyProperty.Register("DateTimeOptionsVisbility", typeof(Visibility), typeof(GenericList<T>), new PropertyMetadata(Visibility.Collapsed));


        public Visibility IntOptionsVisbility
        {
            get { return (Visibility)GetValue(IntOptionsVisbilityProperty); }
            set { SetValue(IntOptionsVisbilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IntOptionsVisbility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntOptionsVisbilityProperty =
            DependencyProperty.Register("IntOptionsVisbility", typeof(Visibility), typeof(GenericList<T>), new PropertyMetadata(Visibility.Collapsed));



        public GenericList()
        {
            list.IsLiveFiltering = true;
            SortCommand = new(sort, param => true);
            UpdateSortOptions();
        }
        void UpdateSortOptions()
        {
            SortOption = typeof(T).GetProperties().Where(prop => prop.PropertyType.IsEnum || prop.PropertyType == typeof(DateTime)).Select(prop => prop.Name).ToList();
        }
        void sort(object param)
        {
            if (typeof(T).GetProperty(selectedSort).PropertyType == typeof(DateTime))
                DateTimeOptionsVisbility = Visibility.Visible;
            else
                IntOptionsVisbility = Visibility.Visible;





        }


    }
}
