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
    public class SortInputVM : DependencyObject, INotifyPropertyChanged
    {

        public List<string> KindOfSort { get; set; } = new() { "Range", "single" };
        private string selectedKind;
        public Visble StringSortVisibility { get; set; } = new();
        public Visble VisibilityKindOfSort { get; set; } = new();

        private string modelContain;
        public string ModelContain
        {
            get => modelContain;
            set
            {
                modelContain = value;
                FilTerNow();
                onPropertyChanged("ModelContain");
            }
        }
        public string SelectedKind
        {
            get => selectedKind;
            set
            {
                selectedKind = value;
            }
        }
        public Action FilTerNow;
        public SortInputVM(Action Filter)
        {
            FilTerNow = Filter;
        }

        public void ValueTypeSort(object param)
        {
            MessageBox.Show((param as ComboBox).SelectedValue.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}
