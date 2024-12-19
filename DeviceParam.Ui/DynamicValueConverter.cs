using System.Globalization;
using System.Windows.Data;

namespace DeviceParam.Ui
{
    public class DynamicValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var wrapper = parameter as DynamicPropertyWrapper;
            if (wrapper == null || value == null) return null;

            return wrapper[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
