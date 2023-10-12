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
        public int Length { get; protected set; }
        public Ship(string username)
        {
            Username = username;
        }
    }

}
