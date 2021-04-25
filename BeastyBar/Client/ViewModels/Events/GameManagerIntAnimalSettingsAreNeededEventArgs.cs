namespace Client.ViewModels.Events
{
    using System;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;

    public class GameManagerIntAnimalSettingsAreNeededEventArgs : EventArgs
    {
        public readonly IntAnimalSettingsProvider Settings;

        public GameManagerIntAnimalSettingsAreNeededEventArgs(IntAnimalSettingsProvider settings)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }
    }
}
