using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Miotsukushi.Converter
{
    class HPStatusTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double nowhp;
            double maxhp;
            int soku;
            bool is_friend;

            if (double.TryParse(values[0].ToString(), out nowhp) && double.TryParse(values[1].ToString(), out maxhp) && int.TryParse(values[2].ToString(), out soku))
                if (nowhp <= 0)
                {
                    bool.TryParse(values[3].ToString(), out is_friend);
                    return soku == 0 ? "破壊" : is_friend ? "轟沈" : "撃沈";
                }
                else if (nowhp <= maxhp * 0.25)
                    return soku == 0 ? "損壊" : "大破";
                else if (nowhp <= maxhp * 0.5)
                    return soku == 0 ? "損害" : "中破";
                else if (nowhp <= maxhp * 0.75)
                    return soku == 0 ? "混乱" : "小破";
                else if (nowhp < maxhp)
                    return "健在";
                else
                    return "無傷";
            else
                return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[0];
        }
    }
}
