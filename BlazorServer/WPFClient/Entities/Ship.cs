using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Components;
using WPFClient.Entities.Component;

namespace WPFClient.Entities
{
    public abstract class Ship : Componentumas
    {
        public string Username { get; protected set; }
        public int Length { get; protected set; }
        public Ship(string username)
        {
            Username = username;
        }
        public override bool IsComposite()
        {
            return false;
        }
        public override int GetLength()
        {
            return Length;
        }
    }

}
