using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class MapSearcher : SearchItemPackFactory
    {
        public readonly ThingFactory thingFactory;

        public readonly TerrainFactory terrainFactory;

        public MapSearcher(ThingFactory thingFactory, TerrainFactory terrainFactory)
        {
            this.thingFactory = thingFactory;
            this.terrainFactory = terrainFactory;
        }

        public override IEnumerable<SearchItemPack> GetThingItemPack(Map map, Filter<Thing> filter)
        {
            return from thing in thingFactory.GetThings(map)
                   where filter.IsRight(thing)
                   let searchItem = new SearchItem(thing)
                   group searchItem by (searchItem.Def, searchItem.Stuff) into searchItems

                   select new SearchItemPack(searchItems);
        }

        public override IEnumerable<SearchItemPack> GetTerrainItemPack(Map map, Filter<TerrainDef> filter)
        {
            return from terrainAndIndex in terrainFactory.GetTerrains(map)
                                           .Where(t => filter.IsRight(t))
                                           .Select((t, index) => (Terrain: t, Index: index))
                   let cell = CellIndicesUtility.IndexToCell(terrainAndIndex.Index, map.Size.x)
                   where !cell.Fogged(map)
                   group cell by terrainAndIndex.Terrain into cellsByTerrain

                   let def = cellsByTerrain.Key
                   let searchItem = new SearchItem(def, cellsByTerrain)
                   select new SearchItemPack(searchItem);
        }
    }
}