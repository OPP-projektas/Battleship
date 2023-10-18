using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;
using WPFClient.Entities.Singelton;

namespace WPFClient.Entities.Observer
{
    class ConcreteObserver : IObserver
    {
        Logger logger = Logger.GetInstance();
        HubConnection lobbyConnection = SignalRConnectionManager.GetInstance().LobbyConnection;
        string username = UserInfo.Username;

        public void Update(ISubject subject)
        {
            if((subject as Subject).State)
            {
                Message message = new Message();
                message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
                logger.Log(message);
                if (lobbyConnection.State != HubConnectionState.Connected)
                {
                    var result = Task.Run(async () => await OpenPlayerLobbyConnection(lobbyConnection));
                    result.Wait();
                }
                var a = Task.Run(async () => await PlayerReady(username));
                a.Wait();

            }
        }
        private async Task OpenPlayerLobbyConnection(HubConnection lobbyConnection)
        {
            try
            {
                await lobbyConnection.StartAsync();
            }
            catch (Exception ex)
            {
            }
        }
        private async Task PlayerReady(string username)
        {
            try
            {
                await lobbyConnection.InvokeAsync("PlayerReady", username);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
