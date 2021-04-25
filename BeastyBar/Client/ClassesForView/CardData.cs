namespace Client.ClassesForView
{
    using System;
    using System.IO;
    using System.Windows.Media;

    public struct CardData
    {
        private Brush fill;

        private Brush stroke;

        private double height;

        private double width;

        public CardData(Brush fill, Brush stroke, double height, double width)
        {
            this.fill = fill ?? throw new ArgumentNullException(nameof(fill));
            this.stroke = stroke ?? throw new ArgumentNullException(nameof(stroke));
            this.height = height;
            this.width = width;
        }

        public Brush Fill
        {
            get
            {
                return this.fill;
            }
        }

        public Brush Stroke
        {
            get
            {
                return this.stroke;
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }
        }

        public SolidColorBrush HoverLayer
        {
            get
            {
                return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        public double ButtonHeight
        {
            get
            {
                return (this.Height * 0.2);
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }
        }
    }
}
