using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
  public static class Tabs
    {
        public static ObservableCollection<TabItemFormat> TabItems { get; set; } = new();
        public static void CloseTab()
        {

        }
    }
}
