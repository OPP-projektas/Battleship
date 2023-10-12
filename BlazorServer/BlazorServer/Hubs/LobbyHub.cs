using Microsoft.AspNetCore.SignalR;

namespace BlazorServer.Hubs
{
    public class LobbyHub : Hub
    {
        public Task AddToTotal(string user, int value)
        {
            return Clients.All.SendAsync("CounterIncrement", user, value);
        }

        public Task AdmitPlayer(string user)
        {
            return Clients.All.SendAsync("AddPlayerToLobby", user);
        }
        public Task PlayerReady(string user)
        {
            return Clients.All.SendAsync("PlayerReadyMessage", user);
        }
    }
}
