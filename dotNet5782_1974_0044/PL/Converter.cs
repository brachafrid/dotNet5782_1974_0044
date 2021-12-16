using IBL.BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    public partial class DroneWindow : UserControl
    {
        private Visibility ConverterAvailable(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            if (drone.DroneState == DroneState.AVAILABLE)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }
        private Visibility ConverterMaintenance(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            if (drone.DroneState == DroneState.MAINTENANCE)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }
        private Visibility ConverterDelivery(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = (IBL.BO.Drone)((FrameworkElement)e.OriginalSource).DataContext;
            if (drone.DroneState == DroneState.DELIVERY)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }
    }
}
