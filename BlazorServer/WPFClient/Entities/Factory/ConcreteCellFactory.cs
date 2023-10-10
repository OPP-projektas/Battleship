using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Facotries
{
    public class ConcreteCellFactory : CellFactory
    {
        public override IFactory GetCell(CellType type)
        {
            switch (type)
            {
                case CellType.Occupied:
                    return new OccupiedCell();
                case CellType.Free:
                    return new FreeCell();
                default:
                    return null;
            }
        }
    }
}
