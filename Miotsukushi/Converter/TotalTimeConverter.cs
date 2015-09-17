using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace Miotsukushi.Converter
{
    sealed class TotalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                var t = (TimeSpan)value;
                if (t.TotalHours < 0)
                    return "00:00";
                else if (t.TotalHours < 1)
                    return Math.Floor(t.TotalMinutes).ToString("00") + ":" + t.Seconds.ToString("00");
                else
                    return Math.Floor(t.TotalHours).ToString("00") + ":" + t.Minutes.ToString("00") + ":" + t.Seconds.ToString("00");
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
