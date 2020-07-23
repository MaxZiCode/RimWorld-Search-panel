﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                        group thing by (thing.def, thing.Stuff)

                        into grouped
                        select new SearchItem(grouped.Key.def, grouped.Select(t => t.Position), grouped.Count(), grouped.Key.Stuff);

            var terrains = from cell in notFoggedCells
                           let terrain = cell.GetTerrain(map)
                           group (terrain, cell) by terrain

                           into grouped
                           select new SearchItem(grouped.Key, grouped.Select(g => g.cell), grouped.Count());

            return items.Concat(terrains).OrderBy(item => item.Label);
        }
    }
}
