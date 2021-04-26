
namespace Client.Models
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
    using System.Runtime;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using BeastyBarGameLogic.GamePlayer;
    using Client.Services;
    using Client.ViewModels;
    using Client;
    using BeastyBarGameLogic.NetworkMessaging;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Represents the main view model for the client game.
    /// </summary>
    /// <seealso cref="Client.ViewModels" />
    public class ClientHandler
    {
        /// <summary>
        /// This field is used to save the URL service.
        /// </summary>
        private readonly UrlService urlService;

        /// <summary>
        /// This field is used to save my access token.
        /// </summary>
        private string myAccessToken;

        /// <summary>
        /// This field is used to save the hub connection.
        /// </summary>
        private HubConnection hubConnection;

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
        /// This field is used to indicate whether it is clients turn.
        /// </summary>
        private bool myTurn = false;

        /// <summary>
        /// This field is used to save the timer.
        /// </summary>
        private System.Timers.Timer timer;

        public event EventHandler<GameCardReceivedEventArgs> GameCardReceived;

        protected void FireGameCardReceived(MessageData data)
        {
            this.GameCardReceived?.Invoke(this, new GameCardReceivedEventArgs(data));
        }

        private bool activeStatus;

        public bool ActiveStatus
        {
            get
            {
                return this.activeStatus;
            }
            set
            {
                this.activeStatus = value;
            }
        }

        private RestService restService = new RestService();

        public ClientHandler(UrlService urlService)
        {
            this.timer = new System.Timers.Timer();
            this.urlService = urlService;
            this.ClientConnected = false;

            // object as a command parameter is needed because:
            // when a xaml object calls a command, the object needs to be relayed to the command method.
            this.SetupCommand = new Command(async obj => await this.Setup());
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
        /// Gets or sets the current game status.
        /// </summary>
        /// <value>
        /// The current game status.
        /// </value>
        public MessageData CurrentGameStatus { get; set; }

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
            }
        }

        public bool GameIsActive { get; private set; }

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

                this.hubConnection.On<string>("StatusMessage", this.OnStatusMessageReceived);
                this.hubConnection.On<MessageData>("GameStatus", this.OnGameCardReceived);

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
        /// Called when a game status has been received from the server.
        /// </summary>
        /// <param name="status">The status.</param>
        private void OnGameCardReceived(MessageData data)
        {
            this.FireGameCardReceived(data);
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


        private async Task ExecuteSendPlayingCard(MessageData data)
        {
            this.timer.Stop();
            if (this.GameIsActive)
            {
                if (this.myTurn)
                {
                    this.myTurn = false;

                    try
                    {
                        await this.hubConnection.SendAsync("UpdatePlayerPlayedCard", data);
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
    }
}
