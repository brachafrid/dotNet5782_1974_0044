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
        /// <summary>
        /// Tab item format key
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Tab item format header
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// Tab item format content the VM
        /// </summary>
        public IDisposable Content { get; set; }
        public void Dispose()
        {
            Content.Dispose();
        }
    }
}
