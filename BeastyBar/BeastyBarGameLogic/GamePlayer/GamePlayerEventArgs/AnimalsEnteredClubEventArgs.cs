namespace BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class AnimalsEnteredClubEventArgs : EventArgs
    {
        public readonly List<PlayerIdAnimalStruct> EnteredAnimals;

        public AnimalsEnteredClubEventArgs(List<PlayerIdAnimalStruct> enteredAnimals)
        {
            this.EnteredAnimals = enteredAnimals ?? throw new ArgumentNullException(nameof(enteredAnimals));
        }
    }
}
