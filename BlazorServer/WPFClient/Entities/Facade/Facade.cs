using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPFClient.Entities.Adapter;
using WPFClient.Entities.Command;

namespace WPFClient.Entities.Facade
{
    public class Facade
    {
        protected HubConnection _connection;
        protected Adapter.MediaPlayer _mediaAdapter;
        protected Invoker _commandInvoker;

        public Facade(HubConnection connection, Adapter.MediaPlayer mediaAdapter)
        {
            _connection = connection;
            _mediaAdapter = mediaAdapter;
        }
        public Facade(Invoker invoker, Adapter.MediaPlayer mediaAdapter)
        {
            _commandInvoker = invoker;
            _mediaAdapter = mediaAdapter;
        }
        public async void Shoot()
        {
            _mediaAdapter.PlayFullVolume("yeah_boy.mp3");
            await _connection.InvokeAsync("Shoot", Player.Username, Shot.CellName);
        }
        public void PlaceShip()
        {
            _commandInvoker.DoCommand();
            _mediaAdapter.PlayFullVolume("yeah_boy.mp3");
        }
    }
}
