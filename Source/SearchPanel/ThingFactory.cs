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
        public virtual IEnumerable<Thing> GetThings(Map map)
        {
            return from thing in map.GetDirectlyHeldThings()
                   select thing;
        }
    }
}