using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterTerrainNone : Filter<Terrain>
    {
        public FilterTerrainNone(Filter<Terrain> child = null) : base(child)
        {
        }

        protected override bool FilterFunc(Terrain item) => false;
    }
}