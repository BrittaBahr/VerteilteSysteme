namespace BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class GameEndedEventArgs : EventArgs
    {
        public readonly List<VictoryPlayerStruct> VictoryBoard;

        public GameEndedEventArgs(List<VictoryPlayerStruct> victoryBoard)
        {
            this.VictoryBoard = victoryBoard ?? throw new ArgumentNullException(nameof(victoryBoard));
        }
    }
}
