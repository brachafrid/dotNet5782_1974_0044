using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using PL.PO;
namespace PL
{
    public class SortInputVM : DependencyObject, INotifyPropertyChanged
    {

        public List<string> KindOfSort { get; set; } = new() { "Range", "Single" };
        public Array WeightCategories { get; set; } = Enum.GetValues(typeof(WeightCategories));
        public Array PrioritiesArray { get; set; } = Enum.GetValues(typeof(Priorities));
        public Array DroneState { get; set; } = Enum.GetValues(typeof(DroneState));
        public Visble VisibilityKindOfSort { get; set; } = new();
        public Visble StringSortVisibility { get; set; } = new();
        public Visble VisibilityWeightCategories { get; set; } = new();
        public Visble VisibilityPriorities { get; set; } = new();
        public Visble VisibilityDroneState { get; set; } = new();

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
        public string SelectedKind { get; set; }
        public string selectedValue { get; set; }
        public string selectedWeight { get; set; }
        public string selectedPiority { get; set; }
        public string selectedState { get; set; }
        public Type TypeOfSelectedParameter { get; set; }

        public Action FilTerNow;
        public SortInputVM(Action Filter)
        {
            FilTerNow = Filter;
        }

        public void ShowValueOfSort(object param)
        {
            selectedValue = (param as ComboBox).SelectedValue.ToString();
            if (selectedValue == "Range")
            {
                MessageBox.Show(selectedValue);
                return;
            }
            ShowValueFilter();

        }

        private void ShowValueFilter()
        {
            switch (TypeOfSelectedParameter.Name)
            {
                case "double":
                    break;
                case "WeightCategories":
                    VisibilityWeightCategories.visibility = Visibility.Visible;
                    break;
                case "Priorities":
                    VisibilityPriorities.visibility = Visibility.Visible;
                    break;
                case "DroneState":
                    VisibilityDroneState.visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public void FilterWeightCategories(object param)
        {
            selectedWeight = (param as ComboBox).SelectedValue.ToString();
            FilTerNow();
        }
        public void FilterPriority(object param)
        {
            selectedPiority = (param as ComboBox).SelectedValue.ToString();
            FilTerNow();
        }
        public void FilterDroneState(object param)
        {
            selectedState = (param as ComboBox).SelectedValue.ToString();
            FilTerNow();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string properyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));

        }
    }
}
