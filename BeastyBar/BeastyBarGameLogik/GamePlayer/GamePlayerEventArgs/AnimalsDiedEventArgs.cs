namespace BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class AnimalsDiedEventArgs : EventArgs
    {
        public readonly List<Animal> DiedAnimals;

        public AnimalsDiedEventArgs(List<Animal> diedAnimals)
        {
            this.DiedAnimals = diedAnimals ?? throw new ArgumentNullException(nameof(diedAnimals));
        }
    }
}
