using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public class MapSearcher : SearchItemFactory
    {
        public override IEnumerable<SearchItem> GetSearchItems()
        {
            Map map = Current.Game.CurrentMap;
            var notFoggedCells = map.AllCells.Where(cell => !map.fogGrid.IsFogged(cell));
            var items = from cell in notFoggedCells
                        from thing in map.thingGrid.ThingsListAtFast(cell)
                        group cell by thing

                        into cellsByThing
                        let thing = cellsByThing.Key
                        let thingsWithCells = new ObjectWithCells(thing, cellsByThing)
                        group thingsWithCells by (thing.def, thing.Stuff)

                        into thingsWithCellsByDefAndStuff
                        let def = thingsWithCellsByDefAndStuff.Key.def
                        let stuff = thingsWithCellsByDefAndStuff.Key.Stuff
                        select new SearchItem(def, thingsWithCellsByDefAndStuff, thingsWithCellsByDefAndStuff.Count(), stuff);

            var terrains = from cell in notFoggedCells
                           let terrain = cell.GetTerrain(map)
                           group cell by terrain

                           into cellsbyTerrain
                           let terrainsWithCells = new ObjectWithCells(cellsbyTerrain.Key, cellsbyTerrain)
                           let def = cellsbyTerrain.Key
                           select new SearchItem(def, new[] { terrainsWithCells }, cellsbyTerrain.Count());

            return items.Concat(terrains).OrderBy(item => item.Label);
        }
    }
}
