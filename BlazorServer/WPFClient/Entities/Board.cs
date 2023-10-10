using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Facotries;

namespace WPFClient.Entities
{
    class Board
    {
        public IFactory[,] boardMatrix = new IFactory[6,6];
        public Board()
        {
            Debug.WriteLine("board started");

            CellFactory concreteCellFactory = new ConcreteCellFactory();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    var concreteCell = concreteCellFactory.GetCell(CellFactory.CellType.Free);

                    concreteCell.Place();

                    boardMatrix[i, j] = concreteCell;
                }
            }
            Debug.WriteLine("board created");
        }
    }
}
