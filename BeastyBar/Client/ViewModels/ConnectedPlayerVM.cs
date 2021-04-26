namespace Client.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Client.ClassesForView;

    public class ConnectedPlayerVM
    {
        private readonly string nickname;

        private readonly int playerId;

        private AlphaRedGreenBlue colour;

        public ConnectedPlayerVM(int playerId, string nickname, AlphaRedGreenBlue colour)
        {
            this.playerId = playerId;
            this.nickname = nickname;
            this.colour = colour;
            this.IsOnTurn = false;
        }

        public string Nickname
        {
            get
            {
                return this.nickname;
            }
        }

        public int PlayerId
        {
            get
            {
                return this.playerId;
            }
        }

        public AlphaRedGreenBlue Colour
        {
            get
            {
                return this.colour;
            }
        }

        public bool IsOnTurn
        {
            get;
            set;
        }
    }
}
