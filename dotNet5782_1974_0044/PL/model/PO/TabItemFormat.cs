using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class TabItemFormat : IDisposable
    {
        public int? Id { get; set; }
        public string Header { get; set; }
        public IDisposable Content { get; set; }
        public void Dispose()
        {
            Content.Dispose();
        }
    }
}
