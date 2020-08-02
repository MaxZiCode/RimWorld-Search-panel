using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class ThingFactory
    {
        private static List<IThingHolder> pawnChildHoldersChache = new List<IThingHolder>();

        public virtual IEnumerable<Thing> GetThings(Map map)
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
    }
}