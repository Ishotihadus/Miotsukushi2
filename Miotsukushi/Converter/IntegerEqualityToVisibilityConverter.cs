using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Miotsukushi.Converter
{
    class IntegerEqualityToVisibilityConverter : IntegerEqualityConverter<Visibility>
    {
        public IntegerEqualityToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        { }
    }
}
