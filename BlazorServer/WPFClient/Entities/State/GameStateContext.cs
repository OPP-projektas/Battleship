using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.Entities.State
{
    public class GameStateContext
    {
        private GameState _state = null;

        public GameStateContext(GameState state)
        {
            this.TransitionTo(state);
        }

        public void TransitionTo(GameState state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        public void ChangeGamePageRequest(Page page, Board? board)
        {
            this._state.HandleGamePageChange(page, board);
        }
    }
}
