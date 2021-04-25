namespace Client.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;
    using Client.ViewModel;

    public class GameManagerAnimalsDiedEventArgs : EventArgs
    {
        public readonly List<AnimalVM> DiedAnimals;

        public GameManagerAnimalsDiedEventArgs(List<AnimalVM> diedAnimals)
        {
            this.DiedAnimals = diedAnimals ?? throw new ArgumentNullException(nameof(diedAnimals));
        }
    }
}
