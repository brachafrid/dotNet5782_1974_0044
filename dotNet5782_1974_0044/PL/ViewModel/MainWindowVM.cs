using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class MainWindowVM
    {
        public RelayCommand CommandAdministration { get; set; }
        public MainWindowVM()
        {
            CommandAdministration = new RelayCommand( ,null)
        }
        public void AdminiustratorEnter()
        {

        }
    }
}
