using System.Windows;

namespace Miotsukushi.Converter
{
    sealed class BooleanToVisibilityReverseConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityReverseConverter() :
            base(Visibility.Collapsed, Visibility.Visible) { }
    }
}
