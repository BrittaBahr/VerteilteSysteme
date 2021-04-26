using BeastyBarGameLogic.NetworkMessaging;
using System;
using System.Threading;

namespace Client.Models
{
    public class GameCardReceivedEventArgs : EventArgs
    {
        private MessageData data;

        public GameCardReceivedEventArgs(MessageData data)
        {
            this.Data = data;
        }

        public MessageData Data 
        { 
            get
            {
                return this.data;
            }

            set
            {
                this.data = value ?? throw new ArgumentNullException("It can not be null.");
            }
        }
    }
}