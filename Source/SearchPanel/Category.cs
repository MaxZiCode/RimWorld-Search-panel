using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SearchPanel
{
    public class Category
    {
        private readonly Filter<Thing> filterThing;
        private readonly Filter<TerrainDef> filterTerrain;
        private readonly Searcher<Thing> thingSearcher;
        private readonly Searcher<TerrainDef> terrainSearcher;

        public Category(Filter<Thing> filterThing, Filter<TerrainDef> filterTerrain, Searcher<Thing> thingSearcher, Searcher<TerrainDef> terrainSearcher)
        {
            this.filterThing = filterThing;
            this.filterTerrain = filterTerrain;
            this.thingSearcher = thingSearcher;
            this.terrainSearcher = terrainSearcher;
        }

        public IEnumerable<SearchItem<Thing>> GetSearchThings(Map map)
        {
            thingSearcher.UpdateItems(map, filterThing);
            return thingSearcher.GetSearchItems();
        }

        public IEnumerable<SearchItem<TerrainDef>> GetSearchTerrains(Map map)
        {
            terrainSearcher.UpdateItems(map, filterTerrain);
            return terrainSearcher.GetSearchItems();
        }
    }
}