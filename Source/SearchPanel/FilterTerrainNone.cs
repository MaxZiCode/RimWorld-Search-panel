using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterTerrainNone : Filter<TerrainDef>
    {
        public FilterTerrainNone(Filter<TerrainDef> child = null) : base(child)
        {
        }

        protected override bool FilterFunc(TerrainDef item) => false;
    }
}