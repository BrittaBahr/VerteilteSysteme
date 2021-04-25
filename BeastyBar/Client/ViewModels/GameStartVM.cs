
namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Media;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using Client.Models;
    using Client.Services;
    using Client.ViewModels;
    using GameLibrary;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Represents the main view model for the client game.
    /// </summary>
    /// <seealso cref="Client.ViewModels.BaseVM" />
    public class GameStartVM : INotifyPropertyChanged
    {
        /// <summary>
        /// This field is used to save the URL service.
        /// </summary>
        private readonly UrlService urlService;

        /// <summary>
        /// This field is used to save the logger.
        /// </summary>
        private readonly ILogger<GameStartVM> logger;

        /// <summary>
        /// This field is used to save my access token.
        /// </summary>
        private string myAccessToken;

        /// <summary>
        /// This field is used to save the hub connection.
        /// </summary>
        private HubConnection hubConnection;

        /// <summary>
        /// This field is used to save the player list.
        /// </summary>
        private ObservableCollection<PlayerVM> playerList;

        /// <summary>
        /// This field is used to save the game list.
        /// </summary>
        private ObservableCollection<SimpleGameInformation> gameList;

        /// <summary>
        /// This field is used to save whether the game is active.
        /// </summary>
        private bool gameIsActive;

        /// <summary>
        /// This field is used to save the client player.
        /// </summary>
        private PlayerVM clientPlayer;

        /// <summary>
        /// This field is used to save the client connected.
        /// </summary>
        private bool clientConnected;

        /// <summary>
        /// This field is used to save whether the game was requested.
        /// </summary>
        private bool gameWasRequested;

        /// <summary>
        /// This field is used to save the status message.
        /// </summary>
        private string statusMessage;

        /// <summary>
        /// This field is used to save the active player name.
        /// </summary>
        private string activePlayerName = string.Empty;

        /// <summary>
        /// This field is used to save the requesting or the enemy player.
        /// </summary>
        private PlayerVM requestingorEnemyPlayer;

        /// <summary>
        /// This field is used to indicate whether it is clients turn.
        /// </summary>
        private bool myTurn = false;

        /// <summary>
        /// This field is used to save the timer.
        /// </summary>
        private System.Timers.Timer timer;

        private bool activeStatus;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void FireOnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool ActiveStatus
        {
            get
            {
                return this.activeStatus;
            }
            set
            {
                this.activeStatus = value;
                this.FireOnPropertyChanged();
            }
        }

        private RestService restService = new RestService();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameStartVM"/> class.
        /// </summary>
        /// <param name="urlService">The URL service.</param>
        /// <param name="logger">The logger.</param>
        public GameStartVM(UrlService urlService, ILogger<GameStartVM> logger)
        {
            this.timer = new System.Timers.Timer();
            this.logger = logger;
            this.urlService = urlService;
            this.PlayerList = new ObservableCollection<PlayerVM>();
            this.GameList = new ObservableCollection<SimpleGameInformation>();
            this.ClientConnected = false;
            this.GameIsActive = false;
            this.GameWasRequested = false;

            // object as a command parameter is needed because:
            // when a xaml object calls a command, the object needs to be relayed to the command method.
            this.SetupCommand = new Command(async obj => await this.Setup());
            this.PlayerClick = new Command(async obj => await this.ExecutePlayerClickAsync((GameCellVM)obj));
            this.AcceptCommand = new Command(async obj => await this.ExecuteAcceptCommand());
            this.StartGameCommand = new Command(async obj => await this.ExecuteStartGame());
            this.DeclineCommand = new Command(async obj => await this.ExecuteDeclineCommand());
            this.ReturnToLobbyCommand = new Command(async obj => await this.ExecuteReturnToLobbyCommand());
            this.ConnectCommand = new Command(async obj => await this.ExecuteConnectCommand());
        }

        /// <summary>
        /// The id needed to update wins accordingly.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets the setup command.
        /// </summary>
        /// <value>
        /// The setup command.
        /// </value>
        public ICommand SetupCommand { get; }

        /// <summary>
        /// Gets the player click command.
        /// This command is used when a game element button is clicked.
        /// Checks if the player is allowed to place his sign and sends the information to the server.
        /// </summary>
        /// <value>
        /// The player click command.
        /// </value>
        public ICommand PlayerClick { get; }

        /// <summary>
        /// Gets the accept command.
        /// When the client accepts a game request, a correspondent message is sent to the server.
        /// </summary>
        /// <value>
        /// The accept command.
        /// </value>
        public ICommand AcceptCommand { get; }

        /// <summary>
        /// Gets the decline command.
        /// When the client declines a game request, a correspondent message is sent to the server.
        /// The requesting player is set to default (null) and the game request boolean is set to false.
        /// </summary>
        /// <value>
        /// The decline command.
        /// </value>
        public ICommand DeclineCommand { get; }

        /// <summary>
        /// Gets the return to lobby command.
        /// This command is used when the player using the client clicks on the return to lobby button.
        /// The game on the client is reset and a request will be sent to the server containing the id of the client player and the id of the enemy player.
        /// </summary>
        /// <value>
        /// The return to lobby command.
        /// </value>
        public ICommand ReturnToLobbyCommand { get; }

        /// <summary>
        /// Gets the request game command.
        /// This command is used when the player using the client requests a game with another online player.
        /// A game request will be sent to the server containing the id of the enemy player and the id of the client player.
        /// </summary>
        /// <value>
        /// The request game command.
        /// </value>
        public ICommand StartGameCommand { get; }

        /// <summary>
        /// Gets the connect command.
        /// This command is used when the player types in his username and connects to the server.
        /// A request will be sent to the server containing the client player name.
        /// </summary>
        /// <value>
        /// The connect command.
        /// </value>
        public ICommand ConnectCommand { get; }

        /// <summary>
        /// Gets or sets the player that has sent a request to play with the client or is actively playing with the client.
        /// Is set to default (null) if there is neither a request or an active game.
        /// </summary>
        /// <value>
        /// The requesting or enemy player.
        /// </value>
        public PlayerVM RequestingOrEnemyPlayer
        {
            get
            {
                return this.requestingorEnemyPlayer;
            }

            set
            {
                this.requestingorEnemyPlayer = value;
                this.FireOnPropertyChanged();
            }
        }

        public string Token
        {
            get
            {
                return this.myAccessToken;
            }
            set
            {
                this.myAccessToken = value;
            }
        }

        /// <summary>
        /// Gets or sets the current game status.
        /// </summary>
        /// <value>
        /// The current game status.
        /// </value>
        public GameStatus CurrentGameStatus { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public int RequestID { get; set; }

        /// <summary>
        /// Gets or sets the name of the active player.
        /// </summary>
        /// <value>
        /// The name of the active player.
        /// </value>
        public string ActivePlayerName
        {
            get
            {
                return this.activePlayerName;
            }

            set
            {
                this.activePlayerName = value;
                this.FireOnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a specific status message to display in the client.
        /// E.g. a player has declined a game request.
        /// Disappears from the client after a set amount of time.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }

            set
            {
                this.statusMessage = value;
                this.FireOnPropertyChanged();

                if (value != string.Empty)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(10000);
                        this.StatusMessage = string.Empty;
                    });
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a game with the client has been requested by another player.
        /// Needed for UI representation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if game was requested; otherwise, <c>false</c>.
        /// </value>
        public bool GameWasRequested
        {
            get
            {
                return this.gameWasRequested;
            }

            set
            {
                this.gameWasRequested = value;
                if (value)
                {
                    this.ActiveStatus = true;
                }

                this.FireOnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player using the client is connected to the server.
        /// </summary>
        /// <value>
        ///   <c>true</c> if client is connected; otherwise, <c>false</c>.
        /// </value>
        public bool ClientConnected
        {
            get
            {
                return this.clientConnected;
            }

            set
            {
                this.clientConnected = value;
                this.FireOnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the player that is using the client.
        /// </summary>
        /// <value>
        /// The client player.
        /// </value>
        /// <exception cref="ArgumentNullException">ClientPlayer - The client player can´t be null.</exception>
        public PlayerVM ClientPlayer
        {
            get
            {
                return this.clientPlayer;
            }

            set
            {
                this.clientPlayer = value ?? throw new ArgumentNullException(nameof(this.ClientPlayer), "The client player can´t be null.");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a game with another player is currently in progress.
        /// </summary>
        /// <value>
        ///   <c>true</c> if a game with another player is currently in progress; otherwise, <c>false</c>.
        /// </value>
        public bool GameIsActive
        {
            get
            {
                return this.gameIsActive;
            }

            set
            {
                this.gameIsActive = value;
                this.FireOnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the list of currently available players.
        /// </summary>
        /// <value>
        /// The list of currently available players.
        /// </value>
        public ObservableCollection<PlayerVM> PlayerList
        {
            get
            {
                return this.playerList;
            }

            set
            {
                this.playerList = value;
                this.FireOnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the list of players currently in a game.
        /// </summary>
        /// <value>
        /// The list of players currently in a game.
        /// </value>
        public ObservableCollection<SimpleGameInformation> GameList
        {
            get
            {
                return this.gameList;
            }

            set
            {
                this.gameList = value;
                this.FireOnPropertyChanged();
            }
        }

        /// <summary>
        /// Resets the game field.
        /// </summary>
        private void ResetField()
        {
        }

        /// <summary>
        /// Closes the connection asynchronously.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private Task CloseConnectionAsync() => this.hubConnection?.DisposeAsync() ?? Task.CompletedTask;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task Setup()
        {
            try
            {
                await this.CloseConnectionAsync();

                this.hubConnection = new HubConnectionBuilder()
                    .WithUrl(this.urlService.LobbyAddress)
                    .Build();

                this.hubConnection.On<List<Player>>("ReceivePlayersAsync", this.OnPlayersReceived);
                this.hubConnection.On<List<SimpleGameInformation>>("ReceiveGames", this.OnGamesReceived);
                this.hubConnection.On<GameRequest>("GameRequested", this.OnGameRequestReceived);
                this.hubConnection.On<Player>("ReturnPlayerInstance", this.OnClientPlayerInstanceReturned);
                this.hubConnection.On<string>("StatusMessage", this.OnStatusMessageReceived);
                this.hubConnection.On<GameStatus>("GameStatus", this.OnGameStatusReceived);
                this.hubConnection.On("EnemyLeftGame", this.OnEnemyLeftGame);
                this.hubConnection.On("DuplicateName", this.OnDuplicateName);

                await this.hubConnection.StartAsync();
            }
            catch (HttpRequestException)
            {
                this.StatusMessage = "Can not reach the server.";
            }
            catch (Exception)
            {
                this.statusMessage = "An unknown error occured. Please try again later.";
            }
        }

        /// <summary>
        /// Called when the client player instance is returned in order to obtain the clients connection id.
        /// </summary>
        /// <param name="player">The client player.</param>
        private void OnClientPlayerInstanceReturned(Player player)
        {
            this.ClientPlayer.Player = player;
        }

        /// <summary>
        /// Called when the client receives a list of currently running games.
        /// </summary>
        /// <param name="games">The games.</param>
        private void OnGamesReceived(List<SimpleGameInformation> games)
        {
            this.GameList = new ObservableCollection<SimpleGameInformation>(games);
        }

        /// <summary>
        /// Called when a name already in the list of players has been chosen.
        /// </summary>
        private void OnDuplicateName()
        {
            this.ClientConnected = false;
            this.StatusMessage = "Duplicate name, please choose a new one.";
        }

        /// <summary>
        /// Called when a game status has been received from the server.
        /// </summary>
        /// <param name="status">The status.</param>
        private void OnGameStatusReceived(GameStatus status)
        {
            this.logger.LogInformation("[OnGameStatusReceived] GameId: {0}", new object[] { status.GameId });
            
            if (this.CurrentGameStatus == null || status.IsNewGame)
            {
                if (this.ClientPlayer.Player.ConnectionId == status.CurrentPlayerId)
                {
                   // this.PlayerTwo = this.RequestingOrEnemyPlayer;
                    //this.PlayerOne = this.ClientPlayer;
                }
                else
                {
                    //this.PlayerOne = this.RequestingOrEnemyPlayer;
                   // this.PlayerTwo = this.ClientPlayer;
                }

                this.GameIsActive = true;
            }

            if (this.ClientPlayer.Player.ConnectionId == status.CurrentPlayerId)
            {
                this.myTurn = true;
                this.timer = new System.Timers.Timer(10000) { AutoReset = false };
                this.timer.Start();
                this.timer.Elapsed += this.Timer_Elapsed;
            }

            this.CurrentGameStatus = status;

            if (this.ClientPlayer.Player.ConnectionId == status.CurrentPlayerId)
            {
                this.ActivePlayerName = this.ClientPlayer.PlayerName;
            }
            else
            {
                this.ActivePlayerName = this.RequestingOrEnemyPlayer.PlayerName;
            }

            if (status.CurrentPlayerMarker == 1)
            {
            }
            else
            {
            }

            if (status.IndexedGame.All<int>(x => x == 0))
            {
                this.ResetField();
            }

          //  this.PlayerOne.Wins = status.WinsPlayerOne;
          //  this.PlayerTwo.Wins = status.WinsPlayerTwo;
        }

        /// <summary>
        /// Handles the Elapsed event of the Timer control. Is responsible for the timeout message if a client player is AFK in a game.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.myTurn)
            {
                this.StatusMessage = "Your turn. Play, or game ends in 5 seconds!";

                Task.Run(() =>
                {
                    this.timer = new System.Timers.Timer(5000) { AutoReset = false };
                    this.timer.Start();

                    this.timer.Elapsed += async (sender, e) =>
                    {
                        this.timer.Stop();
                        this.StatusMessage = "Game ended because of inactivity.";
                        await this.ExecuteReturnToLobbyCommand();
                    };
                });

                this.timer.Stop();
            }

            this.timer.Stop();
        }

        /// <summary>
        /// Called when a status message has been received from the server.
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnStatusMessageReceived(string message)
        {
            this.StatusMessage = message;
        }

        /// <summary>
        /// Called when an enemy has left the game. Resets the properties responsible for the view.
        /// </summary>
        private void OnEnemyLeftGame()
        {
            this.timer.Stop();
            this.logger.LogInformation("[OnEnemyLeftGame]");
            this.StatusMessage = "Enemy left the game.";
         //   this.PlayerOne = new PlayerVM(new Player());
         //   this.PlayerTwo = new PlayerVM(new Player());
            this.GameIsActive = false;
            this.ResetField();
        }

        /// <summary>
        /// Called when a game request has been received from the server. Sets the properties for the view to enable declining or accepting.
        /// </summary>
        /// <param name="gameRequest">The game request.</param>
        private void OnGameRequestReceived(GameRequest gameRequest)
        {
            this.logger.LogInformation("[OnGameRequestReceived] Player {0} requests a game with player {1}", new object[] { gameRequest.RequestingPlayer.PlayerName, gameRequest.Enemy.PlayerName });

            if (gameRequest.Enemy != null)
            {
                this.RequestingOrEnemyPlayer = new PlayerVM(gameRequest.RequestingPlayer);
                this.GameWasRequested = true;
                this.RequestID = gameRequest.RequestID;

                // allow the player to accept or decline a game for 10 seconds (timeout)
                var task = Task.Run(() =>
                {
                    var aTimer = new System.Timers.Timer(95000) { AutoReset = false };

                    aTimer.Start();

                    aTimer.Elapsed += (sender, e) =>
                    {
                        this.GameWasRequested = false;

                        //NEW
                        this.ActiveStatus = false;
                    };
                });
            }
        }

        /// <summary>
        /// Called when the list of players has been received from the server.
        /// </summary>
        /// <param name="players">The players.</param>
        private void OnPlayersReceived(List<Player> players)
        {
            if (this.ClientConnected)
            {
                this.logger.LogInformation("[OnPlayersReceived]");
                this.PlayerList = this.ConvertPlayerListToPlayerVMCollection(players.Where(id => id.ConnectionId != this.ClientPlayer.Player.ConnectionId).ToList());
            }
        }

        /// <summary>
        /// This command is used when the player types in his username and connects to the server.
        /// A request will be sent to the server containing the client player name.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecuteConnectCommand()
        {
            this.logger.LogInformation("[ExecuteConnectCommand]");

            if (!string.IsNullOrEmpty(this.clientPlayer.PlayerName))
            {
                try
                {

                    await this.hubConnection.SendAsync("AddPlayer", this.clientPlayer.PlayerName, this.ClientId);

                    this.ClientConnected = true;
                }
                catch (HttpRequestException)
                {
                    this.StatusMessage = "Unable to connect to server.";
                }
                catch (Exception)
                {
                    this.statusMessage = "An unknown error occured. Please try again later.";
                }
            }
        }

        /// <summary>
        /// When the client accepts a game request, a correspondent message is sent to the server.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecuteAcceptCommand()
        {
            this.logger.LogInformation("[ExecuteAcceptCommand]");
            this.GameWasRequested = false;

            try
            {
                await this.hubConnection.SendAsync("DeclineOrAcceptRequest", this.RequestID, true);
            }
            catch (HttpRequestException)
            {
                this.StatusMessage = "Unable to reach server. Please try again later.";
            }
            catch (Exception)
            {
                this.statusMessage = "An unknown error occured. Please try again later.";
            }

            //NEW
            this.ActiveStatus = false;
        }

        /// <summary>
        /// When the client declines a game request, a correspondent message is sent to the server.
        /// The requesting player is set to default (null) and the game request boolean is set to false.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecuteDeclineCommand()
        {
            this.logger.LogInformation("[ExecuteDeclineCommand]");
            this.GameWasRequested = false;
            this.RequestingOrEnemyPlayer = default;

            try
            {
                await this.hubConnection.SendAsync("DeclineOrAcceptRequest", this.RequestID, false);
            }
            catch (HttpRequestException)
            {
                this.StatusMessage = "Unable to reach server. Please try again later.";
            }
            catch (Exception)
            {
                this.statusMessage = "An unknown error occured. Please try again later.";
            }

            this.RequestID = 0;
            this.ActiveStatus = false;
        }

        /// <summary>
        /// This command is used when the player using the client clicks on the return to lobby button.
        /// The game on the client is reset and a request will be sent to the server containing the id of the client player and the id of the enemy player.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecuteReturnToLobbyCommand()
        {
            this.timer.Stop();
            this.timer = new System.Timers.Timer { AutoReset = false };
            this.myTurn = false;

            this.logger.LogInformation("[ExecuteReturnToLobbyCommand]");

            Application.Current.Dispatcher.Invoke(new ThreadStart(() =>
            {
               // this.PlayerOne = new PlayerVM(new Player());
               // this.PlayerTwo = new PlayerVM(new Player());
                this.GameIsActive = false;
                this.ResetField();
            }));

            try
            {
                await this.hubConnection.SendAsync("ReturnToLobby", this.ClientPlayer.Player.ConnectionId, this.RequestingOrEnemyPlayer.Player.ConnectionId);
            }
            catch (HttpRequestException)
            {
                this.StatusMessage = "Unable to reach server. Please try again later.";
            }
            catch (Exception)
            {
                this.statusMessage = "An unknown error occured. Please try again later.";
            }
        }

        /// <summary>
        /// This command is used when a game element button is clicked.
        /// Checks if the player is allowed to place his sign and sends the information to the server.
        /// </summary>
        /// <param name="cell">The cell that was clicked.</param>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecutePlayerClickAsync(GameCellVM cell)
        {
            this.timer.Stop();

            this.logger.LogInformation("[ExecutePlayerClick] CellIndex: {0}", new object[] { cell.Index });


                if (this.GameIsActive)
                {
                    if (this.CurrentGameStatus.IndexedGame[cell.Index] == 0 && this.CurrentGameStatus.CurrentPlayerId == this.ClientPlayer.Player.ConnectionId && this.myTurn)
                    {
                        cell.PlayerMark = this.CurrentGameStatus.CurrentPlayerMarker;
                        this.myTurn = false;

                        var status = new GameStatus
                        {
                            CurrentPlayerId = this.ClientPlayer.Player.ConnectionId,
                            UpdatedPosition = cell.Index,
                            GameId = this.CurrentGameStatus.GameId
                        };

                        this.ActivePlayerName = this.RequestingOrEnemyPlayer.PlayerName;

                        try
                        {
                            await this.hubConnection.SendAsync("UpdateGameStatus", status);
                        }
                        catch (HttpRequestException)
                        {
                            this.StatusMessage = "Unable to reach server. Please try again later.";
                        }
                        catch (Exception)
                        {
                            this.StatusMessage = "An unknown error occured. Please try again later.";
                        }
                    }
                }                  
        }

        /// <summary>
        /// This command is used to start the game with the online players
        /// A game request will be sent to the server containing the id of the enemy player and the id of the client player.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecuteStartGame()
        {

        }

        /// <summary>
        /// Converts the player list to a collection of player view models.
        /// </summary>
        /// <param name="playerList">The player list.</param>
        /// <returns>The converted collection.</returns>
        private ObservableCollection<PlayerVM> ConvertPlayerListToPlayerVMCollection(List<Player> playerList)
        {
            var collection = new ObservableCollection<PlayerVM>();

            foreach (var player in playerList)
            {
                collection.Add(new PlayerVM(player));
            }

            return collection;
        }

        public void StartNewBotgame()
        {
            this.ResetField();
            this.ClientPlayer.Player.MarkedPositions = new List<int>();
            //this.PlayerOne = this.ClientPlayer;

            //if (this.PlayerTwo == null)
            //{
            //    this.PlayerTwo = new PlayerVM(new Player("Bot"));
            //}
            //else
            //{
            //    this.PlayerTwo = new PlayerVM(new Player("Bot")) { Wins = this.PlayerTwo.Wins };
            //}
        }
    }
}
