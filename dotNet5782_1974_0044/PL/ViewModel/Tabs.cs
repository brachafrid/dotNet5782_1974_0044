using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public static class Tabs
    {
       
        public static ObservableCollection<TabItemFormat> TabItems { get; set; } = new();
        public static Action<int> changeSelectedTab;
        public static void CloseTab(string Text)
        {
            TabItems.Remove(TabItems.FirstOrDefault(tab => tab.Text == Text));
        }
        public static void AddTab(TabItemFormat tabItemFormat)
        {
            TabItemFormat tabItem = TabItems.FirstOrDefault(tab => tab.Text == tabItemFormat.Text);
            if (tabItem == default)
            {
                TabItems.Add(tabItemFormat);
                changeSelectedTab?.Invoke(TabItems.IndexOf(tabItemFormat));
            }

            else
                changeSelectedTab?.Invoke(TabItems.IndexOf(tabItem));
        }
    }
}
