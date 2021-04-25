namespace BeastyBarGameLogic.GamePlayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.Animals.Species;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;
    using BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs;
    using BeastyBarGameLogic.GamePlayer.GamePlayerException;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class BeastyBarPlayer
    {
        public readonly string Name;

        public readonly int PlayerId;

        private readonly IBeastyBarPlayerNetworkCommunicator communicator;

        private readonly Random rand;

        private readonly List<PlayerIdNicknameStruct> otherPlayers;

        private readonly AnimalSettingsManagerVisitor animalSettingManager;

        private PlayerIdNicknameStruct currentActivePlayer;

        private List<Animal> deck;

        private List<Animal> handcards;

        private List<PlayerIdAnimalStruct> queue;

        private List<PlayerIdAnimalStruct> clubAnimals;

        public BeastyBarPlayer(int playerId, string name, IBeastyBarPlayerNetworkCommunicator communicator, Random rand)
        {
            if (string.IsNullOrEmpty(name) | string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name is not allowed to be null, empty or withe space.", nameof(name));
            }

            this.PlayerId = playerId;
            this.Name = name;
            this.rand = rand ?? throw new ArgumentNullException(nameof(rand));
            this.animalSettingManager = new AnimalSettingsManagerVisitor();
            this.animalSettingManager.AnimalAnimalSettingsAreNeeded += AnimalSettingManagerFiredAnimalAnimalSettingsAreNeeded;
            this.animalSettingManager.IntAnimalSettingsAreNeeded += AnimalSettingManagerFiredIntAnimalSettingsAreNeeded;
            //this.communicator = communicator ?? throw new ArgumentNullException(nameof(communicator));
            //List<PlayerIdNicknameStruct> otherPlayers = this.communicator.GetListOfOtherPlayers();

            //if (otherPlayers == null | otherPlayers.Count < 1)
            //{
            //    throw new ArgumentException("There must be at least one other player.", nameof(otherPlayers));
            //}

            //this.otherPlayers = otherPlayers;
            this.clubAnimals = new List<PlayerIdAnimalStruct>();
            this.Queue = new List<PlayerIdAnimalStruct>();
            Chameleon cham = new Chameleon();
            cham.AnimalAnimalSettingsAreNeeded += ChameleonFiredAnimalAnimalSettingsAreNeeded;
            cham.IntAnimalSettingsAreNeeded += ChameleonFiredIntAnimalSettingsAreNeeded;
            this.queue = new List<PlayerIdAnimalStruct>();
            this.deck = new List<Animal>()
            {
                new Lion(),
                new Hippo(),
                new Crocodile(),
                new Snake(),
                new Giraffe(),
                new Zebra(),
                new Seal(),
                cham,
                new Monkey(),
                new Kangaroo(),
                new Parrot(),
                new Stunk()
            };

            this.Handcards = new List<Animal>();

            for (int i = 0; i < 5; i++)
            {
                this.GetRandomHandcard();
            }
        }

        public string CurrentActivePlayerName
        {
            get
            {
                return this.currentActivePlayer.Nickname;
            }
        }

        public List<Animal> Handcards
        {
            get
            {
                return this.handcards;
            }

            private set
            {
                this.handcards = value ?? throw new ArgumentNullException(nameof(this.handcards)); ;
            }
        }

        public List<PlayerIdAnimalStruct> Queue
        {
            get
            {
                return this.queue;
            }

            private set
            {
                this.queue = value ?? throw new ArgumentNullException(nameof(this.queue));
            }
        }

        public event EventHandler<NewHandcardPickedEventArgs> NewHandcardPicked;

        public event EventHandler<AnimalAnimalSettingsAreNeededEventArgs> AnimalAnimalSettingsAreNeeded;

        public event EventHandler<IntAnimalSettingsAreNeededEventArgs> IntAnimalSettingsAreNeeded;

        public event EventHandler<QueueChangedEventArgs> QueueChanged;

        public event EventHandler<AnimalsEnteredClubEventArgs> AnimalsEnteredClub;

        public event EventHandler<AnimalsDiedEventArgs> AnimalsDied;

        public event EventHandler<GameEndedEventArgs> GameEnded;

        public void PlayCard(Animal handcardAnimal)
        {
            //this.currentActivePlayer = this.communicator.GetCurrentPlayer();

            //if (!this.otherPlayers.Contains(this.currentActivePlayer))
            //{
            //    throw new NotValidPlayerException();
            //}

            //if (this.currentActivePlayer.PlayerId != this.PlayerId)
            //{
            //    throw new NotOnTurnException();
            //}

            if (!this.handcards.Contains(handcardAnimal))
            {
                throw new NotValidCardException();
            }

            handcardAnimal = handcardAnimal.Accept(this.animalSettingManager); // looking if further information are needed
            List<Animal> animals = this.Queue.Select(x => x.Animal).ToList();
            animals = handcardAnimal.Queue(animals, -1, false);
            this.UpdateQueue(animals, handcardAnimal);
            this.Handcards.Remove(handcardAnimal);
            //this.communicator.SendPlayedAnimal(this.PlayerId, handcardAnimal);
            this.DoAnimalPushingInQueue();
            this.GetIntoClub();
            this.GetRandomHandcard();
        }

        private void UpdateQueue(List<Animal> animals, Animal changedAnimal)
        {
            List<PlayerIdAnimalStruct> newQueue = new List<PlayerIdAnimalStruct>();

            if (this.Queue.Count == 0)
            {
                this.Queue.Add(new PlayerIdAnimalStruct(this.PlayerId, this.Name, changedAnimal));
                this.OnQueueChanged(this.Queue);
                return;
            }

            List<Animal> killedAnimals = new List<Animal>();

            for (int i = 0; i < animals.Count; i++)
            {
                Animal a1 = animals[i];

                if (!a1.Life)
                {
                    killedAnimals.Add(a1);
                    animals.Remove(a1);
                    --i;
                    continue;
                }

                PlayerIdAnimalStruct pA2 = this.Queue.Find(x => x.Animal == a1);

                if (pA2.Animal == null & pA2.PlayerId == 0)
                {
                    newQueue.Insert(i, new PlayerIdAnimalStruct(this.PlayerId, this.Name, changedAnimal));                    
                }
                else
                {
                    newQueue.Insert(i, pA2);
                }
            }

            this.Queue = newQueue;
            this.OnQueueChanged(this.Queue);

            if (killedAnimals.Count > 0)
            {
                this.OnAnimalsDied(killedAnimals);
            }
        }

        private void DoAnimalPushingInQueue()
        {
            List<Animal> alreadyMoved = new List<Animal>();

            for (int i = 0; i < this.Queue.Count; i++)
            {
                Animal a = this.Queue[i].Animal;

                if (a.SuperPower & !alreadyMoved.Contains(a))
                {
                    List<Animal> animals = this.Queue.Select(x => x.Animal).ToList();
                    animals = a.Queue(animals, i, true).Where(x => x.Life).ToList();
                    this.UpdateQueue(animals, a);
                    alreadyMoved.Add(a);
                    --i;
                }
            }
        }

        private void GetIntoClub()
        {
            if (this.Queue.Count < 5)
            {
                return;
            }

            if (this.Queue.Count > 5)
            {
                throw new ArgumentOutOfRangeException("The Queue can never be greater than 5.", nameof(this.Queue));
            }

            List<PlayerIdAnimalStruct> frontAnimals = this.Queue.TakeLast(2).ToList();
            PlayerIdAnimalStruct killedAnimal = this.Queue.ElementAt(0);
            this.clubAnimals.AddRange(frontAnimals);
            this.OnAnimalsEnteredClub(frontAnimals);
            this.OnAnimalsDied(new List<Animal>{ killedAnimal.Animal });
            this.Queue.Remove(killedAnimal);

            foreach (PlayerIdAnimalStruct a in frontAnimals)
            {
                this.Queue.Remove(a);
            }

            this.OnQueueChanged(this.Queue);
        }

        private void GetRandomHandcard()
        {
            if (this.Handcards.Count == 0)
            {
                //this.communicator.SendNoHandcards(this.PlayerId);
                this.GetGameEnded(true);
            }

            // the player must have between 0 and 5 handcards
            if (this.Handcards.Count >= 0 & this.Handcards.Count < 5)
            {
                if (this.deck.Count == 0)
                {
                    return;
                }

                int index = this.rand.Next(0, this.deck.Count);
                Animal a = this.deck.ElementAt(index);
                this.Handcards.Add(a);
                this.deck.RemoveAt(index);
                this.OnNewHandcardPicked(this.Handcards);
            }
        }

        private void GetPlayedAnimalFromPlayer()
        {
            PlayerIdAnimalStruct playerAnimal = this.communicator.GetPlayedAnimal();

        }

        private void GetGameEnded(bool isGameEnded)
        {
            //bool isGameEnded = this.communicator.GetGameEnded();

            if (isGameEnded)
            {
                List<VictoryPlayerStruct> victories = new List<VictoryPlayerStruct>();

                foreach (PlayerIdAnimalStruct a in this.clubAnimals)
                {
                    bool isInVictories = false;
                    int indexVic = -1;

                    for (int i = 0; i < victories.Count; i++)
                    {
                        VictoryPlayerStruct p = victories[i];

                        if (p.Id == a.PlayerId)
                        {
                            isInVictories = true;
                            indexVic = i;
                            break;
                        }
                    }

                    if (isInVictories)
                    {
                        victories[indexVic].AnimalsInClub.Add(a.Animal);
                        continue;
                    }

                    victories.Add(new VictoryPlayerStruct(a.Nickname, a.PlayerId, new List<Animal>() { a.Animal }));
                }

                this.OnGameEnded(victories);
            }
        }

        private void AnimalSettingManagerFiredIntAnimalSettingsAreNeeded(object sender, IntAnimalSettingsAreNeededEventArgs e)
        {
            this.OnIntAnimalSettingsAreNeeded(e.Settings);
        }

        private void AnimalSettingManagerFiredAnimalAnimalSettingsAreNeeded(object sender, AnimalAnimalSettingsAreNeededEventArgs e)
        {
            this.OnAnimalAnimalSettingsAreNeeded(e.Settings);
        }

        private void ChameleonFiredIntAnimalSettingsAreNeeded(object sender, IntAnimalSettingsAreNeededEventArgs e)
        {
            this.OnIntAnimalSettingsAreNeeded(e.Settings);
        }

        private void ChameleonFiredAnimalAnimalSettingsAreNeeded(object sender, AnimalAnimalSettingsAreNeededEventArgs e)
        {
            this.OnAnimalAnimalSettingsAreNeeded(e.Settings);
        }

        protected virtual void OnAnimalAnimalSettingsAreNeeded(AnimalAnimalSettingsProvider settings)
        {
            this.AnimalAnimalSettingsAreNeeded?.Invoke(this, new AnimalAnimalSettingsAreNeededEventArgs(settings));
        }

        protected virtual void OnIntAnimalSettingsAreNeeded(IntAnimalSettingsProvider settings)
        {
            this.IntAnimalSettingsAreNeeded?.Invoke(this, new IntAnimalSettingsAreNeededEventArgs(settings));
        }

        protected virtual void OnNewHandcardPicked(List<Animal> handcards)
        {
            this.NewHandcardPicked?.Invoke(this, new NewHandcardPickedEventArgs(handcards));
        }

        protected virtual void OnQueueChanged(List<PlayerIdAnimalStruct> queue)
        {
            this.QueueChanged?.Invoke(this, new QueueChangedEventArgs(queue));
        }

        protected virtual void OnAnimalsEnteredClub(List<PlayerIdAnimalStruct> enteredAnimals)
        {
            this.AnimalsEnteredClub?.Invoke(this, new AnimalsEnteredClubEventArgs(enteredAnimals));
        }

        protected virtual void OnAnimalsDied(List<Animal> diedAnimals)
        {
            this.AnimalsDied?.Invoke(this, new AnimalsDiedEventArgs(diedAnimals));
        }

        protected virtual void OnGameEnded(List<VictoryPlayerStruct> clubAnimals)
        {
            this.GameEnded?.Invoke(this, new GameEndedEventArgs(clubAnimals));
        }
    }
}
