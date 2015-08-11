using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Miotsukushi.Converter
{
    class IntegerEqualityConverter<T> : IValueConverter
    {
        public IntegerEqualityConverter(T unequalValue, T equalValue)
        {
            Unequal = unequalValue;
            Equal = equalValue;
        }

        public T Unequal { get; set; }
        public T Equal { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is int && parameter is int && (int)value == (int)parameter) ? Equal : Unequal;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is T && EqualityComparer<T>.Default.Equals((T)value, Equal)) ? parameter : null;
        }
    }
}
