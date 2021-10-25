using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomerMaintainer
{
    public class DifferenceColorConverter : IValueConverter
    {
        public object Convert(object amount, Type targetType, object parameter, CultureInfo culture)
        {
            if (amount is int)
            {
                if ((int)amount >= 0)
                    return new SolidColorBrush(Colors.Green);
                else if ((int)amount < 0)
                    return new SolidColorBrush(Colors.Red);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
