namespace Client.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using BeastyBarGameLogic.Animals;

    public class CardRowVM : INotifyPropertyChanged
    {
        private CardVM animal1;

        private CardVM animal2;

        private CardVM animal3;

        private CardVM animal4;

        private CardVM animal5;

        public CardRowVM(CardVM animal1, CardVM animal2, CardVM animal3, CardVM animal4, CardVM animal5)
        {
            this.Animal1 = animal1;
            this.Animal2 = animal2;
            this.Animal3 = animal3;
            this.Animal4 = animal4;
            this.Animal5 = animal5;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CardVM Animal1
        {
            get
            {
                return this.animal1;
            }

            set
            {
                this.animal1 = value;
                this.OnPropertyChanged(nameof(this.Animal1));
            }
        }

        public CardVM Animal2
        {
            get
            {
                return this.animal2;
            }

            set
            {
                this.animal2 = value;
                this.OnPropertyChanged(nameof(this.Animal2));
            }
        }

        public CardVM Animal3
        {
            get
            {
                return this.animal3;
            }

            set
            {
                this.animal3 = value;
                this.OnPropertyChanged(nameof(this.Animal3));
            }
        }

        public CardVM Animal4
        {
            get
            {
                return this.animal4;
            }

            set
            {
                this.animal4 = value;
                this.OnPropertyChanged(nameof(this.Animal4));
            }
        }

        public CardVM Animal5
        {
            get
            {
                return this.animal5;
            }

            set
            {
                this.animal5 = value;
                this.OnPropertyChanged(nameof(this.Animal5));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
