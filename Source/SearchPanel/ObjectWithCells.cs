using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public struct ObjectWithCells
    {
        public object Object { get; set; }

        public IReadOnlyCollection<IntVec3> Cells { get; set; }

        public ObjectWithCells(object @object, IEnumerable<IntVec3> cells)
        {
            Object = @object;
            Cells = cells.ToList();
        }
    }
}
