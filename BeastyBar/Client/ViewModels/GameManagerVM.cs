namespace Client.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Media;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.Animals.Species;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;
    using GameStartVM.ClassesForView;

    public class GameManagerVM : INotifyPropertyChanged
    {
        private Animal animal1;

        private CardVM[] queue;

        private CardVM[] handcards;

        private double cardHeight;

        private double cardWidth;

        private double menuWidth;

        private List<ConnectedPlayerVM> otherPlayers;

        private ConnectedPlayerVM currentPlayer;

        public GameManagerVM()
        {
            this.Animal1 = new Stunk();
            AnimalVM a1 = new AnimalVM(new Lion());
            AnimalVM a2 = new AnimalVM(new Hippo());
            AnimalVM a3 = new AnimalVM(new Zebra());
            AnimalVM a4 = new AnimalVM(new Giraffe());
            AnimalVM b1 = new AnimalVM(new Chameleon());
            AnimalVM b2 = new AnimalVM(new Kangaroo());
            AnimalVM b3 = new AnimalVM(new Parrot());
            AnimalVM b4 = new AnimalVM(new Monkey());
            AlphaRedGreenBlue colour = new AlphaRedGreenBlue(255, 255, 0, 255);
            CardVM c1 = new CardVM(a1, colour, this.CardHeight, this.CardWidth);
            CardVM c2 = new CardVM(a2, colour, this.CardHeight, this.CardWidth);
            CardVM c3 = new CardVM(a3, colour, this.CardHeight, this.CardWidth);
            CardVM c4 = new CardVM(a4, colour, this.CardHeight, this.CardWidth);
            CardVM c5 = new CardVM(null, colour, this.CardHeight, this.CardWidth);

            this.CurrentPlayer = new ConnectedPlayerVM(0, "Svenja", new AlphaRedGreenBlue(255, 51, 51, 255));

            this.CardHeight = 200;
            this.CardWidth = 150;
            this.Queue = new CardVM[]
            {
                c1, c2, c3, c4, c5
            };
            this.Handcards = new CardVM[]
            {
                new CardVM(b1, colour, this.CardHeight, this.CardWidth),
                new CardVM(b2, colour, this.CardHeight, this.CardWidth),
                new CardVM(b3, colour, this.CardHeight, this.CardWidth),
                new CardVM(b4, colour, this.CardHeight, this.CardWidth)
            };

            this.OtherPlayers = new List<ConnectedPlayerVM>()
            {
                new ConnectedPlayerVM(0, "Borni", new AlphaRedGreenBlue(255, 255, 0, 25)),
                new ConnectedPlayerVM(0, "Dani", new AlphaRedGreenBlue(255, 0, 255, 25)),
                new ConnectedPlayerVM(0, "Hendrik", new AlphaRedGreenBlue(255, 0, 0, 255)),
                new ConnectedPlayerVM(0, "Britta", new AlphaRedGreenBlue(255, 30, 50, 25))
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush Test
        {
            get
            {
                return new SolidColorBrush(Color.FromArgb(150, 204, 229, 255));
            }
        }

        public Animal Animal1
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

        public CardVM[] Queue
        {
            get
            {
                return this.queue;
            }

            set
            {
                this.queue = value;
                this.OnPropertyChanged(nameof(this.Queue));
            }
        }

        public CardVM[] Handcards
        {
            get
            {
                return this.handcards;
            }

            set
            {
                this.handcards = value;
                this.OnPropertyChanged(nameof(this.Handcards));
            }
        }
        
        public string PathToClubImage
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\ClubClosed.jpg");
            }
        }

        public string PathToGraveyardImage
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Kill.jpg");
            }
        }

        public string PathToDeckImage
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Deck.jpg");
            }
        }

        public string PathToCoverImage
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Cover.jpg");
            }
        }

        public string PathToBackgroundImage
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Wall.jpg");
            }
        }

        public string PathToBackgroundMenuImage
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\Background.jpg");
            }
        }

        public double CardHeight
        {
            get
            {
                return this.cardHeight;
            }

            set
            {
                this.cardHeight = value;
                this.OnPropertyChanged(nameof(this.CardHeight));
            }
        }

        public double CardWidth
        {
            get
            {
                return this.cardWidth;
            }

            set
            {
                this.cardWidth = value;
                this.OnPropertyChanged(nameof(this.CardWidth));
            }
        }

        public double MenuWidth
        {
            get
            {
                return this.menuWidth;
            }

            set
            {
                this.menuWidth = value;
                this.OnPropertyChanged(nameof(this.MenuWidth));
            }
        }

        public List<ConnectedPlayerVM> OtherPlayers
        {
            get
            {
                return this.otherPlayers;
            }

            private set
            {
                if (value != this.otherPlayers)
                {
                    this.otherPlayers = value ?? throw new ArgumentNullException();
                    this.OnPropertyChanged(nameof(this.OtherPlayers));
                }
            }
        }

        public ConnectedPlayerVM CurrentPlayer
        {
            get
            {
                return this.currentPlayer;
            }

            private set
            {
                if (value != this.currentPlayer)
                {
                    this.currentPlayer = value ?? throw new ArgumentNullException();
                    this.OnPropertyChanged(nameof(this.CurrentPlayer));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
