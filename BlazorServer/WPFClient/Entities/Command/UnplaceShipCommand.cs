using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Command
{
    public class UnplaceShipCommand : ICommand
    {
        private List<Position> _position;
        private Board _board;
        public UnplaceShipCommand(List<Position> position, Board board)
        {
            _position = position;
            _board = board;
        }
        public void Execute()
        {
            foreach (var position in _position)
            {           
                _board.UnoccupyCell(position);
            }
        }

        public void Undo()
        {
            foreach (var position in _position)
            {
                _board.OccupyCell(position);
            }
        }
    }
}
