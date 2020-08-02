using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace SearchPanel
{
    public abstract class SearchItemPackFactory
    {
        public abstract IEnumerable<SearchItemPack> GetTerrainItemPack(Map map, Filter<TerrainDef> filter);

        public abstract IEnumerable<SearchItemPack> GetThingItemPack(Map map, Filter<Thing> filter);
    }
}