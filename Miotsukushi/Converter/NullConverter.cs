using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Miotsukushi.Converter
{
    class NullConverter<T> : IValueConverter
    {
        public NullConverter(T unnullValue, T nullValue)
        {
            Unnull = unnullValue;
            Null = nullValue;
        }

        public T Unnull { get; set; }
        public T Null { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? Unnull : Null;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, Unnull) ? new object() : null;
        }
    }
}
