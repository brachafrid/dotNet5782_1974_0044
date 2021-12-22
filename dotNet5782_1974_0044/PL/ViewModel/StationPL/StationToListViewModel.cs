using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;
using BO;
using BLApi;
using System.Collections.ObjectModel;
using System.Collections;

namespace PL
{
    public class StationToListViewModel
    {
        public ObservableCollection<StationToList> Stations { get; set; }
        public BLApi.IBL ibal;
        private MainWindow MainWindow;

        /// <summary>
        /// Constructor CustomerToList Window.
        /// Initializes necessary things
        /// </summary>
        /// <param name="mainWindow">Main window</param>
        public StationToListViewModel()
        {
            ibal = BLFactory.GetBL();
        }

        public void LoadStations()
        {
            Stations = new ObservableCollection<StationToList>(ibal.GetStations());
        }

        private ICommand _clickCommand;
        private ICommand _doubleClickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler((obj) => CloseTab(obj), (object obj) => true));
            }
        }

        public ICommand DoubleClickCommand
        {
            get
            {
                return _doubleClickCommand ?? (_doubleClickCommand = new CommandHandler((obj) => OpenTab(obj), (object obj) => true));
            }
        }

        public void CloseTab(object obj)
        {
            object mainWindow = Window.GetWindow(obj as StationToListWindowxaml);
            if (mainWindow is MainWindow)
            {
                (mainWindow as MainWindow).StationToListTab.Visibility = Visibility.Collapsed;
                (mainWindow as MainWindow).StationToListControl.Visibility = Visibility.Collapsed;
            }
        }

        public void OpenTab(object obj)
        {
            MessageBox.Show("dddddddd");
        }
    }
}
