namespace Client.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class GameManagerGameEndedEventArgs : EventArgs
    {
        public readonly List<VictoryPlayerStruct> VictoryBoard;

        public GameManagerGameEndedEventArgs(List<VictoryPlayerStruct> victoryBoard)
        {
            this.VictoryBoard = victoryBoard ?? throw new ArgumentNullException(nameof(victoryBoard));
        }
    }
}
