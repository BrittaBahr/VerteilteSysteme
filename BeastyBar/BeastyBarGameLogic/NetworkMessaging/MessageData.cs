using Newtonsoft.Json;
using BeastyBarGameLogic.Animals;
using System;

namespace BeastyBarGameLogic.NetworkMessaging
{

    public class MessageData
    {
        private Animal animal;

        private string currentPlayerId;

        [JsonConstructor]
        public MessageData(string currentPlayerId, Animal animal)
        {
            this.CurrentPlayerId = currentPlayerId;
            this.Animal = animal;
        }

        public string CurrentPlayerId
        {
            get
            {
                return this.currentPlayerId;
            }
            set
            {
                this.currentPlayerId = value ?? throw new ArgumentNullException("It can not be null.");
            }
        }

    public Animal Animal {
        get
        {
            return this.animal;
        }
        set
        {
            this.animal = value ?? throw new ArgumentNullException("It can not be null.");
        }
    }
}
}
