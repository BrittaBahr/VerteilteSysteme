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

    public class PathToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string x)
            {
                return this.GetBrush(x);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        private Brush GetBrush(string path)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(path)));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 96, 96, 96));
            }
        }
    }
}
