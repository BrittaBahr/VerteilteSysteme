
namespace Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using BeastyBarGameLogic.NetworkMessaging;
    using Client.Models;

    /// <summary>
    /// Represents a view model for a player.
    /// </summary>
    /// <seealso cref="GameStartVM.ViewModels.BaseVM" />
    public class PlayerVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerVM"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public PlayerVM(BeastyBarPlayer player)
        {
            this.Player = player;
        }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        /// <value>
        /// The name of the player.
        /// </value>
        public string PlayerName
        {
            get
            {
                return /*this.Player.PlayerName;*/ null;
            }

            set
            {
                this.FireOnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void FireOnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public BeastyBarPlayer Player { get; set; }
    }
}
