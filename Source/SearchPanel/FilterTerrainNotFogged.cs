using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterTerrainNotFogged : Filter<Terrain>
    {
        public FilterTerrainNotFogged(Filter<Terrain> child = null) : base(child)
        {
        }

        protected override bool FilterFunc(Terrain item) => !item.Place.Fogged(item.Map);
    }
}