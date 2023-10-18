using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Singelton
{
    public class SignalRConnectionManager
    {
        private static SignalRConnectionManager _instance;
        public HubConnection LobbyConnection { get; private set; }

        private SignalRConnectionManager()
        {
            LobbyConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7263/lobbyhub")
                .WithAutomaticReconnect()
                .Build();
        }

        public static SignalRConnectionManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SignalRConnectionManager();
            }
            return _instance;
        }
    }
}
