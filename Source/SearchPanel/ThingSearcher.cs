using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class ThingSearcher : Searcher<Thing>
    {
        private static readonly List<IThingHolder> pawnChildHoldersChache = new List<IThingHolder>();

        protected override SearchItem<Thing> CreateSearchItem(Thing item) => new ThingSearchItem(item, GetKey(item));

        protected override IEnumerable<Thing> GetItems(Map map)
        {
            foreach (var thing in map.GetDirectlyHeldThings())
            {
                if (thing is Pawn pawn)
                {
                    pawnChildHoldersChache.Clear();
                    pawn.GetChildHolders(pawnChildHoldersChache);
                    foreach (var pawnThing in pawnChildHoldersChache.SelectMany(holder => holder.GetDirectlyHeldThings()))
                    {
                        yield return pawnThing;
                    }
                }
                yield return thing;
            }
        }

        protected override string GetKey(Thing item) => item is MinifiedThing mThing ? mThing.InnerThing.def.LabelCap : item.def.LabelCap;
    }
}