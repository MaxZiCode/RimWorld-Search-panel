using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class MapSearcher : SearchItemFactory
    {
        public readonly ThingFactory thingFactory;

        public readonly TerrainFactory terrainFactory;

        public MapSearcher(ThingFactory thingFactory, TerrainFactory terrainFactory)
        {
            this.thingFactory = thingFactory;
            this.terrainFactory = terrainFactory;
        }

        public override IEnumerable<SearchItem> GetMapItems(Map map)
        {
            ThingOwner thingOwner = map.GetDirectlyHeldThings();
            return from thing in thingFactory.GetThings(map)
                   group thing by (thing.def, thing.Stuff)

                   into thingsByDef
                   let def = thingsByDef.Key.def
                   let objectWithCells = thingsByDef.Select(t => new ObjectWithCells(t, new[] { t.Position }))
                   let count = thingOwner.TotalStackCountOfDef(def)
                   let stuff = thingsByDef.Key.Stuff
                   select new SearchItem(def, objectWithCells, count, stuff);
        }

        public override IEnumerable<SearchItem> GetTerrains(Map map)
        {
            var terrains = terrainFactory.GetTerrains(map);
            var allTerrainAndCells = terrains.Select((t, index) => (Terrain: t, Cell: CellIndicesUtility.IndexToCell(index, map.Size.x)));

            return from terrainAndCell in allTerrainAndCells
                   group terrainAndCell.Cell by terrainAndCell.Terrain

                   into cellsByTerrain
                   let def = cellsByTerrain.Key
                   let objectWithCells = new[] { new ObjectWithCells(def, cellsByTerrain) }
                   select new SearchItem(def, objectWithCells, cellsByTerrain.Count());
        }
    }
}