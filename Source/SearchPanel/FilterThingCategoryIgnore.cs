using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterThingCategoryIgnore : FilterThingCategory
    {
        public FilterThingCategoryIgnore(ThingCategory thingCategory, Filter<Thing> child = null) : base(thingCategory, child)
        {
        }

        protected override bool FilterFunc(Thing thing) => !base.FilterFunc(thing);
    }
}