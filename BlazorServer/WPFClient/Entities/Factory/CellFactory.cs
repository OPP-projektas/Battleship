using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Facotries
{
    public abstract class CellFactory
    {
        public enum CellType
        {
            Occupied,
            Free
        }
        public abstract IFactory GetCell(CellType type);
    }
}
