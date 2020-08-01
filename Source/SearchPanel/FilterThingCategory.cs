using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterThingCategory : Filter<Thing>
    {
        protected ThingCategory thingCategory;

        public FilterThingCategory(ThingCategory thingCategory, Filter<Thing> child = null) : base(child)
        {
            this.thingCategory = thingCategory;
        }

        protected override bool FilterFunc(Thing thing) => thing.def.category == thingCategory;
    }
}