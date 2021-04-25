namespace BeastyBarGameLogic.GamePlayer.NetworkCommunication
{
    using System;
    using System.Collections.Generic;
    using BeastyBarGameLogic.Animals;

    public struct VictoryPlayerStruct
    {
        public readonly string Nickname;

        public readonly int Id;

        private List<Animal> animalsInClub;

        public VictoryPlayerStruct(string nickname, int id, List<Animal> animalsInClub)
        {
            this.Nickname = nickname;
            this.Id = id;
            this.animalsInClub = animalsInClub ?? new List<Animal>();
        }
        
        public List<Animal> AnimalsInClub
        {
            get
            {
                return this.animalsInClub;
            }

            set
            {
                this.animalsInClub = value ?? throw new ArgumentNullException(nameof(this.animalsInClub));
            }
        }
    }
}
