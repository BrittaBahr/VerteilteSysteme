namespace Client.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Media;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.Animals.Species;
    using BeastyBarGameLogic.GamePlayer;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;
    using BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;
    using Client.ClassesForView;
    using Client.ViewModels.Events;

    public class GameManagerVM : INotifyPropertyChanged
    {
        private readonly BeastyBarPlayer model;

        private readonly AlphaRedGreenBlue colour;

        private CardVM[] queue;

        private CardVM[] handcards;

        private double cardHeight;

        private double cardWidth;

        private double menuWidth;

        private List<ConnectedPlayerVM> otherPlayers;

        private ConnectedPlayerVM currentPlayer;

        public GameManagerVM(BeastyBarPlayer model, AlphaRedGreenBlue colour, List<ConnectedPlayerVM> otherPlayers)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.model.AnimalAnimalSettingsAreNeeded += ModelAnimalAnimalSettingsAreNeeded;
            this.colour = colour;
            this.OtherPlayers = otherPlayers;
            this.Handcards = this.model.Handcards.Select(x => new CardVM(new AnimalVM(x), this.colour, this.CardHeight, this.CardWidth)).ToArray();
            List<CardVM> list = this.model.Queue.Select(x => new CardVM(new AnimalVM(x.Animal), this.GetColourFromPlayerId(x.PlayerId), this.CardHeight, this.CardWidth)).ToList();
            CardVM[] arr = new CardVM[5];

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (i < list.Count)
                {
                    arr[i] = list.ElementAt(i);
                    continue;
                }

                arr[i] = new CardVM(null, new AlphaRedGreenBlue(255, 96, 96, 96), this.CardHeight, this.CardWidth);
            }

            this.Queue = arr;
        }

        public event EventHandler<GameManagerAnimalAnimalSettingsAreNeededEventArgs> AnimalAnimalSettingsAreNeeded;

        public event EventHandler<GameManagerIntAnimalSettingsAreNeededEventArgs> IntAnimalSettingsAreNeeded;

        public event EventHandler<GameManagerAnimalsEnteredClubEventArgs> AnimalsEnteredClub;

        public event EventHandler<GameManagerAnimalsDiedEventArgs> AnimalsDied;

        public event EventHandler<GameManagerGameEndedEventArgs> GameEnded;

        private void ModelAnimalAnimalSettingsAreNeeded(object sender, BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs.AnimalAnimalSettingsAreNeededEventArgs e)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush Test
        {
            get
            {
                return new SolidColorBrush(Color.FromArgb(150, 204, 229, 255));
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

        private AlphaRedGreenBlue GetColourFromPlayerId(int playerId)
        {
            ConnectedPlayerVM p = this.OtherPlayers.Find(x => x.PlayerId == playerId);

            if (p == null)
            {
                //throw new ArgumentNullException("This was not a vlid playerId", nameof(p));
                return new AlphaRedGreenBlue(255, 0, 0, 0);
            }

            return p.Colour;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnAnimalAnimalSettingsAreNeeded(AnimalAnimalSettingsProvider settings)
        {
            this.AnimalAnimalSettingsAreNeeded?.Invoke(this, new GameManagerAnimalAnimalSettingsAreNeededEventArgs(settings));
        }

        protected virtual void OnIntAnimalSettingsAreNeeded(IntAnimalSettingsProvider settings)
        {
            this.IntAnimalSettingsAreNeeded?.Invoke(this, new GameManagerIntAnimalSettingsAreNeededEventArgs(settings));
        }

        protected virtual void OnAnimalsEnteredClub(List<PlayerIdAnimalStruct> enteredAnimals)
        {
            this.AnimalsEnteredClub?.Invoke(this, new GameManagerAnimalsEnteredClubEventArgs(enteredAnimals));
        }

        protected virtual void OnAnimalsDied(List<AnimalVM> diedAnimals)
        {
            this.AnimalsDied?.Invoke(this, new GameManagerAnimalsDiedEventArgs(diedAnimals));
        }

        protected virtual void OnGameEnded(List<VictoryPlayerStruct> clubAnimals)
        {
            this.GameEnded?.Invoke(this, new GameManagerGameEndedEventArgs(clubAnimals));
        }
    }
}
