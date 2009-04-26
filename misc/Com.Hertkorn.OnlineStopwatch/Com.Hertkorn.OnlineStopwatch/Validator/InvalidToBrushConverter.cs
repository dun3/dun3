using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Com.Hertkorn.OnlineStopwatch.Validator
{
    public class InvalidToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ClockValidator validator = value as ClockValidator;
            if (null != validator)
            {
                return new SolidColorBrush((!string.IsNullOrEmpty(validator[parameter.ToString()]) ? Colors.Red : Colors.Transparent));
            }
            return new SolidColorBrush(((bool)value ? Colors.Red : Colors.Transparent));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
