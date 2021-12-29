using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace PL
{
    public class SortInputVM:DependencyObject
    {
       
        public List<string> KindOfSort { get; set; } = new() { "Range", "single" };
        private string selectedKind;

        public string ModelContain { get; set; }
        public string SelectedKind
        {
            get => selectedKind;
            set
            {
                selectedKind = value;
            }
        }
        public SortInputVM()
        {
            VisibilityKindOfSort = new();
        }
      public Visble VisibilityKindOfSort { get; set; }

    }
}
