using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities
{
    public abstract class Ship
    {
        public string Username { get; protected set; }
        Position position;
        int shipLength = 0;
        public Ship(string username)
        {
            Username = username;
        }
    }

}
