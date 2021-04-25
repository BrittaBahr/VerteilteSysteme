namespace BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.Animals;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class QueueChangedEventArgs : EventArgs
    {
        public readonly List<PlayerIdAnimalStruct> Queue;

        public QueueChangedEventArgs(List<PlayerIdAnimalStruct> queue)
        {
            this.Queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }
    }
}
