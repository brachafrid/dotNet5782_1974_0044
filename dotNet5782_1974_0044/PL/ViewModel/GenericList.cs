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
        public string selectedOption { set; get; }
        public RelayCommand SortCommand { set; get; }
        public List<string> Options { set; get; } = new() { "range", "certain" };
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


        public Visibility OptionVisibility
        {
            get { return (Visibility)GetValue(OptionVisibilityProperty); }
            set { SetValue(OptionVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OptionVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OptionVisibilityProperty =
            DependencyProperty.Register("OptionVisibility", typeof(Visibility), typeof(GenericList<T>), new PropertyMetadata(Visibility.Collapsed));
        public GenericList()
        {
            list.IsLiveFiltering = true;
            SortCommand = new(option, param => true);
            UpdateSortOptions();
        }
        void UpdateSortOptions()
        {
            SortOption = typeof(T).GetProperties().Where(prop => prop.PropertyType.IsEnum || prop.PropertyType == typeof(DateTime)).Select(prop => prop.Name).ToList();
        }
        void option(object param)
        {
            OptionVisibility = Visibility.Visible;
        }
        void chooseOption(object param)
        {
             
        }



    }
}
