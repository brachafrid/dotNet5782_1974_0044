using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class Visble : DependencyObject
    {
        public Visble(Visibility visibilityTmp = Visibility.Collapsed)
        {
            if (visibility != Visibility.Collapsed)
                visibility = visibilityTmp;
        }
        public Visibility visibility
        {
            get { return (Visibility)GetValue(visibilityProperty); }
            set { SetValue(visibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty visibilityProperty =
            DependencyProperty.Register("visibility", typeof(Visibility), typeof(Visble), new PropertyMetadata(Visibility.Collapsed));


    }
}
