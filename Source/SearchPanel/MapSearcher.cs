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
                   group thing by (thing.def, thing.Stuff) into thingsByDef

                   let searchItems = from thing in thingsByDef
                                     select new SearchItem(thing)
                   select new SearchItemPack(searchItems);
        }

        public override IEnumerable<SearchItemPack> GetTerrainItemPack(Map map, Filter<TerrainDef> filter)
        {
            return from terrainAndIndex in terrainFactory.GetTerrains(map).Where(t => filter.IsRight(t)).Select((t, index) => (Terrain: t, Index: index))
                   let cell = CellIndicesUtility.IndexToCell(terrainAndIndex.Index, map.Size.x)
                   group cell by terrainAndIndex.Terrain into cellsByTerrain

                   let def = cellsByTerrain.Key
                   let searchItems = from cell in cellsByTerrain
                                     select new SearchItem(def, cell)
                   select new SearchItemPack(searchItems);
        }
    }
}