namespace Client.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using Client.ClassesForView;
    using Client.ViewModel.Events;

    public class CardVM : INotifyPropertyChanged
    {
        private AnimalVM animal;

        private AlphaRedGreenBlue colour;

        private double height;

        private double width;

        public CardVM(AnimalVM animal, AlphaRedGreenBlue colour, double height, double width)
        {
            this.Animal = animal;
            this.colour = colour;
            this.Height = height;
            this.Width = width;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<CardHeightChangedEventArgs> CardHeightChanged;

        public AnimalVM Animal
        {
            get
            {
                return this.animal;
            }

            set
            {
                this.animal = value;
                this.OnPropertyChanged(nameof(this.Animal));
            }
        }

        public AlphaRedGreenBlue Colour
        {
            get
            {
                return this.colour;
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
                this.OnPropertyChanged(nameof(this.Height));
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
                this.OnPropertyChanged(nameof(this.Width));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnCardHeightChanged(double height)
        {
            this.CardHeightChanged?.Invoke(this, new CardHeightChangedEventArgs(height));
        }
    }
}
