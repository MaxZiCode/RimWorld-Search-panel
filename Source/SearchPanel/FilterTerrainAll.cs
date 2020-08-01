using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterTerrainAll : Filter<TerrainDef>
    {
        public FilterTerrainAll(Filter<TerrainDef> child = null) : base(child)
        {
        }

        protected override bool FilterFunc(TerrainDef item) => true;
    }
}