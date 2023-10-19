using Microsoft.AspNetCore.SignalR;

namespace BlazorServer.Hubs
{
    public class LobbyHub : Hub
    {
        private static readonly Dictionary<string, string> usernameToConnectionId = new Dictionary<string, string>();
        private static Dictionary<string, bool> turnDict = new Dictionary<string, bool>();
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
            if (!string.IsNullOrEmpty(user))
            {
                usernameToConnectionId[user] = Context.ConnectionId;
            }
            return Clients.All.SendAsync("PlayerReadyMessage", user);
        }
        public Task PlayerNotReady(string user)
        {
            if (!string.IsNullOrEmpty(user))
            {
                usernameToConnectionId.Remove(user);
            }
            return Clients.All.SendAsync("PlayerNotReadyMessage", user);
        }
        public Task ReportBack(bool report, string coords)
        {
            Clients.All.SendAsync("Hit", report, coords);
            if (!report)
            {
                var result = Task.Run(async () => await ChangeTurn());
                result.Wait();
            }
            return Clients.All.SendAsync("ReportBack", report);
        }

        public async Task StartGame(Dictionary<string,bool> turnDictionary)
        {
            await Clients.All.SendAsync("StartGame", "Ready");
            await Task.Delay(500);
            turnDict = turnDictionary;
            string firstKey = GetKeyAtPosition(turnDictionary, 0);
            string secondKey = GetKeyAtPosition(turnDictionary, 1);


            if (usernameToConnectionId.TryGetValue(firstKey, out string firstTargetConnectionId))
            {
                await Clients.Client(firstTargetConnectionId).SendAsync("DecideTurn", turnDictionary[firstKey]);
            }

            if (usernameToConnectionId.TryGetValue(secondKey, out string secondTargetConnectionId) && secondTargetConnectionId != null)
            {
                
                await Clients.Client(secondTargetConnectionId).SendAsync("DecideTurn", turnDictionary[secondKey]);
            }

        }

        public async Task Shoot(string username, string cellName)
        {
            await Clients.All.SendAsync("Shoot", username, cellName);

            string firstKey = GetKeyAtPosition(turnDict, 0);
            string secondKey = GetKeyAtPosition(turnDict, 1);

            if (turnDict[firstKey])
            {
                if (usernameToConnectionId.TryGetValue(secondKey, out string fisrstTargetConnectionId))
                {
                    await Clients.Client(fisrstTargetConnectionId).SendAsync("CheckIfHit", cellName.Substring(cellName.Length - 2));
                }
            }
            else
            {
                if (usernameToConnectionId.TryGetValue(firstKey, out string secondTargetConnectionId))
                {
                    await Clients.Client(secondTargetConnectionId).SendAsync("CheckIfHit", cellName.Substring(cellName.Length - 2));
                }
            }

        }
        public async Task ChangeTurn()
        {
            string firstKey = GetKeyAtPosition(turnDict, 0);
            string secondKey = GetKeyAtPosition(turnDict, 1);

            turnDict[firstKey] = !turnDict[firstKey];
            turnDict[secondKey] = !turnDict[secondKey];

            if (usernameToConnectionId.TryGetValue(firstKey, out string fisrstTargetConnectionId))
            {
                await Clients.Client(fisrstTargetConnectionId).SendAsync("DecideTurn", turnDict[firstKey]);
            }

            if (usernameToConnectionId.TryGetValue(secondKey, out string secondTargetConnectionId))
            {
                await Clients.Client(secondTargetConnectionId).SendAsync("DecideTurn", turnDict[secondKey]);
            }
        }
        static TKey GetKeyAtPosition<TKey, TValue>(Dictionary<TKey, TValue> dictionary, int position)
        {
            if (position < 0 || position >= dictionary.Count)
            {
                return default(TKey); 
            }

            int index = 0;
            foreach (var key in dictionary.Keys)
            {
                if (index == position)
                {
                    return key;
                }
                index++;
            }

            return default(TKey);
        }

    }
}
