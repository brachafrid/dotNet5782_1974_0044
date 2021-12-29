﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PL
{
    public class SortInputVM:DependencyObject, INotifyPropertyChanged
    {
       
        public List<string> KindOfSort { get; set; } = new() { "Range", "single" };
        private string selectedKind;

        public string SelectedKind
        {
            get => selectedKind;
            set
            {
                VisibilityKindOfSort = Visibility.Visible;
                selectedKind = value;
                onPropertyChanged("SelectedKind");
               
            }
        }
        public SortInputVM()
        {
        }
        public Visibility VisibilityKindOfSort
        {
            get { return (Visibility)GetValue(VisibilityKindOfSortProperty); }
            set { SetValue(VisibilityKindOfSortProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilityKindOfSort.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilityKindOfSortProperty =
            DependencyProperty.Register("VisibilityKindOfSort", typeof(Visibility), typeof(SortInputVM), new PropertyMetadata(Visibility.Collapsed));


        public void ShowKindOfSort(object param)
        {
            VisibilityKindOfSort = Visibility.Visible;
            MessageBox.Show("gggggggggggggggg");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}
