using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Miotsukushi.Converter
{
    class IsKirakiraVisibilityConverter : IValueConverter
    {
        public IsKirakiraVisibilityConverter()
        {

        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int && (int)value > 49)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
