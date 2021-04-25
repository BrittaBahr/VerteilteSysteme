namespace Client.ViewModel.Events
{
    using System;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;

    public class CardHeightChangedEventArgs : EventArgs
    {
        public readonly double Height;

        public CardHeightChangedEventArgs(double height)
        {
            this.Height = height;
        }
    }
}
