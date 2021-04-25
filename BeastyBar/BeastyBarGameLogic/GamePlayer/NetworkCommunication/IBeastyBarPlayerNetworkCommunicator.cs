namespace BeastyBarGameLogic.GamePlayer.NetworkCommunication
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BeastyBarGameLogic.Animals;

    public interface IBeastyBarPlayerNetworkCommunicator
    {
        // PlayerId + Nickname
        List<PlayerIdNicknameStruct> GetListOfOtherPlayers();

        // PlayerId + Nickname
        PlayerIdNicknameStruct GetCurrentPlayer();

        void SendPlayedAnimal(int playerId, Animal playedAnimal);

        // PlayerId + Played Animal
        PlayerIdAnimalStruct GetPlayedAnimal();

        void SendNoHandcards(int playerId);

        bool GetGameEnded();
    }
}
