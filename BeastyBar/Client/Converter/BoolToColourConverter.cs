namespace Client.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class BoolToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool x)
            {
                return this.GetBrush(x);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        private Brush GetBrush(bool value)
        {
            if (value)
            {
                return new SolidColorBrush(Color.FromArgb(255, 0, 204, 102));
            }

            return new SolidColorBrush(Color.FromArgb(255, 204, 0, 0));
        }
    }
}
