

namespace Client.Services
{
    /// <summary>
    /// Contains the connection for the azure service the server is hosted on and a connection for a local host.
    /// </summary>
    public class UrlService
    {
        public string LobbyAddress => $"https://localhost:44384/game";

        public string ApiAddress => $"https://localhost:44384/game";
    }
}
