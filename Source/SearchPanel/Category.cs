using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SearchPanel
{
    public class Category
    {
        protected readonly Filter<Thing> filterThing;
        protected readonly Filter<TerrainDef> filterTerrain;
        protected readonly SearchItemPackFactory searchItemFactory;

        public Category(Filter<Thing> filterThing, Filter<TerrainDef> filterTerrain, SearchItemPackFactory searchItemFactory)
        {
            this.filterThing = filterThing;
            this.filterTerrain = filterTerrain;
            this.searchItemFactory = searchItemFactory;
        }

        public IEnumerable<SearchItemPack> GetItems(Map map)
        {
            var thingItems = searchItemFactory.GetThingItemPack(map, filterThing);
            var terrainItems = searchItemFactory.GetTerrainItemPack(map, filterTerrain);
            return thingItems.Concat(terrainItems);
        }
    }
}