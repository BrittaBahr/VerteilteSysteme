namespace Client.ClassesForView
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.Animals.Species;

    public class AnimalPictureVisitor : IAnimalVisitor<Brush>
    {
        public Brush Visit(Lion lion)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Lion.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Hippo hippo)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Hippo.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Crocodile crocodile)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Crocodile.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Snake snake)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Snake.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Giraffe giraffe)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Giraffe.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Zebra zebra)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Zebra.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Seal seal)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Seal.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Chameleon chameleon)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Chameleon.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Monkey monkey)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Monkey.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Kangaroo kangaroo)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Kangaroo.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Parrot parrot)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Parrot.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }

        public Brush Visit(Stunk stunk)
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Stunk.jpg"))));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            }
        }
    }
}
