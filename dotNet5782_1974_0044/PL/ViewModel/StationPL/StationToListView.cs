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
    public class StationToListView
    {
        public BLApi.IBL ibal;
        public ObservableCollection<StationToList> Stations { get; set; }
        private MainWindow MainWindow;
        /// <summary>
        /// Constructor DroneToList Window.
        /// Initializes necessary things
        /// </summary>
        /// <param name="mainWindow">Main window</param>
        public StationToListView()
        {
            ibal = BLFactory.GetBL();
            //MainWindow = mainWindow;
        }

        public void LoadStations()
        {
            Stations = new ObservableCollection<StationToList>(ibal.GetStations());
        }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler((obj) => MyAction(obj), true));
            }
        }

        public void MyAction(object obj)
        {
            (obj as MainWindow).StationToListTab.Visibility = Visibility.Collapsed;
            MessageBox.Show("fffffffffffff");
        }
    }
    public class CommandHandler : ICommand
    {
        private Action<object> _action;
        private bool _canExecute;
        public CommandHandler(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }

}


