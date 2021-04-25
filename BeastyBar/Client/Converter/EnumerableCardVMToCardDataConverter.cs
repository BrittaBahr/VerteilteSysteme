namespace Client.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Client.ClassesForView;
    using Client.ViewModel;

    public class EnumerableCardVMToCardDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<CardVM> ans)
            {
                AnimalPictureVisitor visitor = new AnimalPictureVisitor();

                return ans.Select(x => this.GetRectangleData(x, visitor)).ToList();
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        private Brush GetBrush(AnimalVM a, AnimalPictureVisitor visitor)
        {
            if (a == null)
            {
                return new SolidColorBrush(Color.FromArgb(100, 200, 200, 200));
            }

            Brush brush = a.Accept(visitor);

            return brush;
        }

        private CardData GetRectangleData(CardVM x, AnimalPictureVisitor visitor)
        {
            if (x.Animal == null)
            {
                return new CardData(this.GetBrush(x.Animal, visitor), new SolidColorBrush(Color.FromRgb(96, 96, 96)), x.Height, x.Width);
            }

            return new CardData(this.GetBrush(x.Animal, visitor), new SolidColorBrush(Color.FromRgb(x.Colour.Red, x.Colour.Green, x.Colour.Blue)), x.Height, x.Width);
        }
    }
}
