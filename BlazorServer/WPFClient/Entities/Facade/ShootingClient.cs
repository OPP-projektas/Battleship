using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Facade
{
    public class ShootingClient
    {
        private Facade _facade;
        public ShootingClient(Facade facade)
        {
            _facade = facade;
        }
        public void Shoot()
        {
            _facade.Shoot();
        }
    }
}
