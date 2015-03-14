using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Converter
{
    class IntegerToVisibilityConverter : IntegerConverter<System.Windows.Visibility>
    {
        public IntegerToVisibilityConverter() :
            base(System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed)
        { }
    }
}
