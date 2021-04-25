namespace BeastyBarGameLogic.GamePlayer.NetworkCommunication
{
    using System;
    using BeastyBarGameLogic.Animals;

    public struct PlayerIdNicknameStruct
    {
        public readonly string Nickname;

        public readonly int PlayerId;

        public PlayerIdNicknameStruct(int playerId, string nickname)
        {
            this.PlayerId = playerId;
            this.Nickname = nickname;
        }
    }
}
