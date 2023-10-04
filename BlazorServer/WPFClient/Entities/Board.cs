using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities
{
    class Board
    {
        public Cell[,] boardMatrix = new Cell[6,6];
        public Board()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    boardMatrix[i, j] = new Cell();
                }
            }
        }
    }
}
