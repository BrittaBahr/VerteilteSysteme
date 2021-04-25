namespace BeastyBarGameLogic.GamePlayer.NetworkCommunication
{
    using System;
    using BeastyBarGameLogic.Animals;

    public struct PlayerIdAnimalStruct
    {
        public readonly Animal Animal;

        public readonly string Nickname;

        public readonly int PlayerId;

        public PlayerIdAnimalStruct(int playerId, string nickname, Animal animal)
        {
            this.PlayerId = playerId;
            this.Nickname = nickname;
            this.Animal = animal ?? throw new ArgumentNullException(nameof(animal));
        }
    }
}
