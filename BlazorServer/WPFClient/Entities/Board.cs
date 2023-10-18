﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Facotries;

namespace WPFClient.Entities
{
    public class Board
    {
        public IFactory[,] boardMatrix = new IFactory[6,6];
        public Board()
        {

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
        }
        public void ReplaceCell(Position position)
        {
            CellFactory concreteCellFactory = new ConcreteCellFactory();
            var concreteCell = concreteCellFactory.GetCell(CellFactory.CellType.Occupied);
            boardMatrix[position._x, position._y] = concreteCell;
        }
        public IFactory GetCellByPosition(Position position)
        {
            return boardMatrix[position._x, position._y];
        }
    }
}
