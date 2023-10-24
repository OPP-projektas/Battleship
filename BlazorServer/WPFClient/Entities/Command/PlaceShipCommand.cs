using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Command
{
    public class PlaceShipCommand : ICommand
    {
        private List<Position> _position;
        private Board _board;
        private TypesOfShips _lastTypeOfShip;
        public PlaceShipCommand(List<Position> position, Board board)
        {
            _position = position;
            _board = board;
        }
        public List<Position> GetLastShipPosition()
        {
            return _position;
        }
        public TypesOfShips GetLastTypeOfShip()
        {
            return _lastTypeOfShip;
        }
        private void SetLastTypeOfShip()
        {
            switch (_position.Count)
            {
                case 1:
                    _lastTypeOfShip = TypesOfShips.Boat;
                    break;
                case 2:
                    _lastTypeOfShip = TypesOfShips.Battleship;
                    break;
                case 3:
                    _lastTypeOfShip = TypesOfShips.Submarine;
                    break;
                case 4:
                    _lastTypeOfShip = TypesOfShips.Carrier;
                    break;
            }
        }
        public void Execute()
        {
            foreach (var position in _position)
            {
                _board.OccupyCell(position);
            }
            SetLastTypeOfShip();
        }
        public void Undo()
        {
            foreach (var position in _position)
            {
                _board.UnoccupyCell(position);
            }
            SetLastTypeOfShip();
        }
    }
}
