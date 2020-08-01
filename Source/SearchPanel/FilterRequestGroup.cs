using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterRequestGroup : Filter<Thing>
    {
        protected ThingRequestGroup thingRequestGroup;

        public FilterRequestGroup(ThingRequestGroup thingRequestGroup, Filter<Thing> child = null) : base(child)
        {
            this.thingRequestGroup = thingRequestGroup;
        }

        protected override bool FilterFunc(Thing thing) => ThingListGroupHelper.Includes(thingRequestGroup, thing.def);
    }
}