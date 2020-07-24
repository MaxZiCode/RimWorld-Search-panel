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
                        group (thing, cell) by (thing.def, thing.Stuff)

                        into grouped
                        let def = grouped.Key.def
                        let cells = grouped.Select(g => g.cell)
                        let distinctThings = grouped.Select(g => g.thing).Distinct()
                        let stuff = grouped.Key.Stuff
                        select new SearchItem(def, cells, distinctThings.Count(), stuff);

            var terrains = from cell in notFoggedCells
                           let terrain = cell.GetTerrain(map)
                           group cell by terrain

                           into grouped
                           let def = grouped.Key
                           let cells = grouped
                           select new SearchItem(def, cells, cells.Count());

            return items.Concat(terrains).OrderBy(item => item.Label);
        }

        public static IEnumerable<IntVec3> GetCells(Thing thing)
        {
            //if (thing.CustomRectForSelector != null)
            //{
            //    foreach (var cell in thing.CustomRectForSelector)
            //        yield return cell;
            //}    
            //else
            //{
            //    yield return thing.Position;
            //}
            return new CellRect(thing.Position.x - thing.RotatedSize.x / 2, thing.Position.z - thing.RotatedSize.z / 2, thing.RotatedSize.x, thing.RotatedSize.z);
        }
    }
}
