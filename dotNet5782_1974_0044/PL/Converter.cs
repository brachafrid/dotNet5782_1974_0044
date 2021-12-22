﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BO;

namespace PL
{
    /// <summary>
    /// Multi Converter for Visibility
    /// </summary>
    public class parcelTreatedByDroneVisibility : IValueConverter
    {
        /// <summary>
        /// Convert per drone state to visibility, visible or collapsed
        /// </summary>
        /// <param name="value">drone's state</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns visibility, visible or collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != DroneState.MAINTENANCE.ToString())
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        /// <summary>
        /// Convert Back for Visibility
        /// </summary>
        /// <param name="value">drone's state</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns visibility, hidden</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Hidden;
        }
    }

    /// <summary>
    /// Converter to update model of drone
    /// </summary>
    public class ConverterUpdateModel : IMultiValueConverter
    {
        /// <summary>
        /// Converts to visibility
        /// If the model name has been updated, converts to visible. else, converts to collapsed.
        /// </summary>
        /// <param name="values">Names of: new drone model , original drone model</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns visibility: visible or collapsed</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] as string !=values[1] as string)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        /// <summary>
        /// Convert Back to update model
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns Exception</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Multi Converter parcel Treated By Drone Content
    /// </summary>
    public class parcelTreatedByDroneContent : IMultiValueConverter
    {
        /// <summary> 
        /// Multi Converter parcel Treated By Drone Content
        /// Checks skimmer status and package status.
        /// Convert to content of the update buttons
        /// </summary>
        /// <param name="values">drone state and parcel state</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns the content of update buttons</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0].ToString() == DroneState.DELIVERY.ToString())
            {
                if (values[1].ToString() == "True")
                {
                    return "Parcel delivery";
                }
                else
                    return "Parcel collection";
            }
            else
                return "Sending the drone for delivery";

        }

        /// <summary> 
        /// Convert Back to parcel Treated By Drone Content
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns Exception</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convert Drone Charging Content
    /// </summary>
    public class ConvertDroneChargingContent : IValueConverter
    {
        /// <summary>
        /// Checks drone status.
        /// Convert to content of the update button
        /// </summary>
        /// <param name="value">drone state</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns the content of update button</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == DroneState.MAINTENANCE.ToString())
                return "Realse drone from charge";
            else
                return "Send drone to charge";

        }

        /// <summary>
        /// Convert Back to Convert Drone Charging Content
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns Exception</returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convert Drone Charging Visibility
    /// </summary>
    public class ConvertDroneChargingVisibility : IValueConverter
    {
        /// <summary>
        /// Checks drone status.
        /// Convert to visibility: if drone state is delivery - visible. else - collapsed
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns visibility: visible or collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != DroneState.DELIVERY.ToString())
                return Visibility.Visible;
            else
                return Visibility.Collapsed;

        }

        /// <summary>
        /// Convert Back to Convert Drone Charging Visibility
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns Exception</returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}