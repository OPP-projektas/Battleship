using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.Entities.State
{
    public abstract class GameState
    {
        protected GameStateContext _context;

        public void SetContext(GameStateContext context)
        {
            this._context = context;
        }

        public abstract void HandleGamePageChange(Page page, Board? board);
    }
}
