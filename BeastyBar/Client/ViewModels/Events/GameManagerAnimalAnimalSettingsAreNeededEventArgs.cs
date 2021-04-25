namespace Client.ViewModels.Events
{
    using System;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;

    public class GameManagerAnimalAnimalSettingsAreNeededEventArgs : EventArgs
    {
        public readonly AnimalAnimalSettingsProvider Settings;

        public GameManagerAnimalAnimalSettingsAreNeededEventArgs(AnimalAnimalSettingsProvider settings)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }
    }
}
