namespace BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.Animals;

    public class NewHandcardPickedEventArgs : EventArgs
    {
        public readonly List<Animal> Handcards;

        public NewHandcardPickedEventArgs(List<Animal> handcards)
        {
            this.Handcards = handcards ?? throw new ArgumentNullException(nameof(handcards));
        }
    }
}
