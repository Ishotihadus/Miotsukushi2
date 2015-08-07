using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Miotsukushi.Converter
{
    class HPStatusColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double nowhp;
            double maxhp;

            if (double.TryParse(values[0].ToString(), out nowhp) && double.TryParse(values[1].ToString(), out maxhp))
                if (nowhp == 0)
                    return Colors.RoyalBlue;
                else if (nowhp <= maxhp * 0.25)
                    return Colors.Crimson;
                else if (nowhp <= maxhp * 0.5)
                    return Colors.DarkOrange;
                else if (nowhp <= maxhp * 0.75)
                    return Colors.Goldenrod;
                else
                    return Colors.MediumSeaGreen;
            else
                return Binding.DoNothing;
        }
        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[0];
        }
    }
}
