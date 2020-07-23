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
        public override IEnumerable<SearchItem> GetSearchItems()
        {
            Map map = Current.Game.CurrentMap;
            var items = from loc in map.AllCells
                        where !map.fogGrid.IsFogged(loc)
                        select map.thingGrid.ThingsListAtFast(loc)

                        into things
                        from thing in things
                        group thing by thing.def

                        into grouped
                        let def = grouped.Key
                        let label = def.LabelCap
                        orderby label.ToString()
                        select new SearchItem
                        {
                            Label = label,
                            Texture = def.uiIcon,
                            Count = grouped.Count(),
                            Def = def
                        };

            return items;
        }
    }
}
