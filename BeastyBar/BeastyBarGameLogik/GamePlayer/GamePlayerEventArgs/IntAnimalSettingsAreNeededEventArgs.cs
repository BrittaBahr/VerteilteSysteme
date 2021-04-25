namespace BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs
{
    using System;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;

    public class IntAnimalSettingsAreNeededEventArgs : EventArgs
    {
        public readonly IntAnimalSettingsProvider Settings;

        public IntAnimalSettingsAreNeededEventArgs(IntAnimalSettingsProvider settings)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }
    }
}
