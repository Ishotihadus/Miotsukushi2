using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Miotsukushi.Converter
{
    class FuelAmmoStatusColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double now_quantity;
            double max_quantity;

            if (double.TryParse(values[0].ToString(), out now_quantity) && double.TryParse(values[1].ToString(), out max_quantity))
                if (now_quantity == 0)
                    return Colors.RoyalBlue;
                else if (now_quantity < max_quantity * 3 / 9)
                    return Colors.Crimson;
                else if (now_quantity < max_quantity * 7 / 9)
                    return Colors.DarkOrange;
                else if (now_quantity < max_quantity)
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
