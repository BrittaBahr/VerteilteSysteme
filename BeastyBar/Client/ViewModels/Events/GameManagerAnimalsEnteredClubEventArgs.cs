namespace Client.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;
    using Client.ViewModel;

    public class GameManagerAnimalsEnteredClubEventArgs : EventArgs
    {
        public readonly List<PlayerIdAnimalStruct> EnteredAnimals;

        public GameManagerAnimalsEnteredClubEventArgs(List<PlayerIdAnimalStruct> enteredAnimals)
        {
            this.EnteredAnimals = enteredAnimals ?? throw new ArgumentNullException(nameof(enteredAnimals));
        }
    }
}
