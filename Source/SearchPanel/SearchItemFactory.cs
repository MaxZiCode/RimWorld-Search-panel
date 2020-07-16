﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace SearchPanel
{
    public static class SearchItemFactory
    {
        public static IEnumerable<SearchItem> GetSearchItems(Map map)
        {
            var items = from loc in map.AllCells
                        select map.thingGrid.ThingsListAtFast(loc)
                        into things
                        from thing in things
                        group thing by thing.def
                        into g
                        let def = g.Key
                        select new SearchItem
                        {
                            Label = def.LabelCap,
                            Texture = def.uiIcon,
                            Count = g.Count(),
                            Def = def
                        };

			return items;
		}
    }
}
