using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class SortInputVM:DependencyObject
    {
        public Visibility EnumOptionsVisbility
        {
            get { return (Visibility)GetValue(IntOptionsVisbilityProperty); }
            set { SetValue(IntOptionsVisbilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnumOptionsVisbility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntOptionsVisbilityProperty =
            DependencyProperty.Register("EnumOptionsVisbility", typeof(Visibility), typeof(SortInputVM), new PropertyMetadata(Visibility.Collapsed));
    }
}
