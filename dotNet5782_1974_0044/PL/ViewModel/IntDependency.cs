using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
   public class IntDependency:DependencyObject
    {

        public int Instance
        {
            get { return (int)GetValue(InstanceProperty); }
            set { SetValue(InstanceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Instance.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InstanceProperty =
            DependencyProperty.Register("Instance", typeof(int), typeof(IntDependency), new PropertyMetadata(null));


    }
}
