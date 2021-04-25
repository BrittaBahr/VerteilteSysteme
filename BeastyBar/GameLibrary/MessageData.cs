
namespace GameLibrary
{
    using Newtonsoft.Json;
    using BeastyBarGameLogic.Animals;

    /// <summary>
    /// This class is used to update the status of a game on the server and on clients.
    /// It contains the current game status, the current player id, the updated position, the id of the game and the wins of the players.
    /// </summary>
    public class MessageData
    {
        [JsonConstructor]
        public MessageData(string currentPlayerId, Animal animal)
        {
            this.CurrentPlayerId = currentPlayerId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageData"/> class.
        /// </summary>
        public MessageData()
        {
            this.UpdatedPosition = -1;
        }

        /// <summary>
        /// Gets or sets the updated position.
        /// </summary>
        /// <value>
        /// The updated position.
        /// </value>
        public int UpdatedPosition { get; set; }

        /// <summary>
        /// Gets or sets the game identifier.
        /// </summary>
        /// <value>
        /// The game identifier.
        /// </value>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the indexed game.
        /// </summary>
        /// <value>
        /// The indexed game.
        /// </value>
        public int[] IndexedGame { get; set; }

        /// <summary>
        /// Gets or sets the current player identifier.
        /// </summary>
        /// <value>
        /// The current player identifier.
        /// </value>
        public string CurrentPlayerId { get; set; }

        /// <summary>
        /// Gets or sets the current player marker.
        /// </summary>
        /// <value>
        /// The current player marker.
        /// </value>
        public int CurrentPlayerMarker { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a new game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a new game; otherwise, <c>false</c>.
        /// </value>
        public bool IsNewGame { get; set; }
    }
}
