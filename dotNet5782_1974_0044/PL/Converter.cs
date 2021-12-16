using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using IBL.BO;

namespace PL
{
    public class ConverterDroneAviable : IValueConverter
    {
        //private Visibility ConverterAvailable(object sender, RoutedEventArgs e)
        //{

        //}
        //private Visibility ConverterMaintenance(object sender, RoutedEventArgs e)
        //{
        //    IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
        //    if (drone.DroneState == DroneState.MAINTENANCE)
        //    {
        //        return Visibility.Visible;
        //    }
        //    else
        //        return Visibility.Collapsed;
        //}
        //private Visibility ConverterDelivery(object sender, RoutedEventArgs e)
        //{
        //    IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
        //    if (drone.DroneState == DroneState.DELIVERY)
        //    {
        //        return Visibility.Visible;
        //    }
        //    else
        //        return Visibility.Collapsed;
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            while ((value as FrameworkElement).GetType() == typeof(Drone))
                value = (value as FrameworkElement).Parent;
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)value).DataContext;
            if (drone.DroneState == DroneState.AVAILABLE)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
