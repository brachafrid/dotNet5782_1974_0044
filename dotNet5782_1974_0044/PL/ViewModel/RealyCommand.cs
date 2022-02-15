using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL
{
    public class RelayCommand :ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        /// <summary>
        /// Can execute changed
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="execute">execute</param>
        /// <param name="canExecute">can Execute</param>
        public RelayCommand (Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// check if can excute
        /// </summary>
        /// <param name="parameter">object</param>
        /// <returns>if can excute</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter = null)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        /// Excute
        /// </summary>
        /// <param name="parameter">object</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
