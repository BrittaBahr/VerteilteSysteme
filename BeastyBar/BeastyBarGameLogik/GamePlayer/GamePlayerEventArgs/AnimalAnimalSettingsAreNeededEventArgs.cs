namespace BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs
{
    using System;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;

    public class AnimalAnimalSettingsAreNeededEventArgs : EventArgs
    {
        public readonly AnimalAnimalSettingsProvider Settings;

        public AnimalAnimalSettingsAreNeededEventArgs(AnimalAnimalSettingsProvider settings)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }
    }
}
