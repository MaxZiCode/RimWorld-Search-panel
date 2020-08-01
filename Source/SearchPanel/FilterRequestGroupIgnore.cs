using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterRequestGroupIgnore : FilterRequestGroup
    {
        public FilterRequestGroupIgnore(ThingRequestGroup thingRequestGroup, Filter<Thing> child = null) : base(thingRequestGroup, child)
        {
        }

        protected override bool FilterFunc(Thing thing) => !base.FilterFunc(thing);
    }
}