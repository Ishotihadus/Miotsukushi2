using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Miotsukushi.Converter
{
    class IntegerConverter<T> : IValueConverter
    {
        public IntegerConverter(T nonZeroValue, T zeroValue)
        {
            True = nonZeroValue;
            False = zeroValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is int && (int)value != 0) ? True : False;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is T && EqualityComparer<T>.Default.Equals((T)value, True)) ? 1 : 0;
        }
    }
}
