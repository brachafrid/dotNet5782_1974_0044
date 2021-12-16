using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using IBL.BO;

namespace PL
{
    class ConverterParcel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as IBL.BO.Drone).Parcel is null)
                return string.Empty;
            return (value as IBL.BO.Drone).Parcel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals( string.Empty))
                return null;
            return value;
        }
    }
}
