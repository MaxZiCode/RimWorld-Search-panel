using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SearchPanel
{
    public class Category
    {
        protected readonly Filter<Thing> filterThing;
        protected readonly Filter<TerrainDef> filterTerrain;
        protected readonly SearchItemFactory searchItemFactory;

        public Category(Filter<Thing> filterThing, Filter<TerrainDef> filterTerrain, SearchItemFactory searchItemFactory)
        {
            this.filterThing = filterThing;
            this.filterTerrain = filterTerrain;
            this.searchItemFactory = searchItemFactory;
        }

        public IEnumerable<SearchItem> GetItems(Map map)
        {
            var thingItems = searchItemFactory.GetThingItems(map);
            var terrainItems = searchItemFactory.GetTerrainItems(map);
            return thingItems.Concat(terrainItems);
        }
    }
}