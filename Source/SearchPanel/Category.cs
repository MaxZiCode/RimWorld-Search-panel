using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SearchPanel
{
    public class Category
    {
        private readonly Filter<Thing> filterThing;
        private readonly Filter<Terrain> filterTerrain;
        private readonly Searcher<Thing> thingSearcher;
        private readonly Searcher<Terrain> terrainSearcher;

        public Category(Filter<Thing> filterThing, Filter<Terrain> filterTerrain, Searcher<Thing> thingSearcher, Searcher<Terrain> terrainSearcher)
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

        public IEnumerable<SearchItem<Terrain>> GetSearchTerrains(Map map)
        {
            terrainSearcher.UpdateItems(map, filterTerrain);
            return terrainSearcher.GetSearchItems();
        }
    }
}