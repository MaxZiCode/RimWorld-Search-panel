using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class FilterThingNotFogged : Filter<Thing>
    {
        public FilterThingNotFogged(Filter<Thing> child = null) : base(child)
        {
        }

        protected override bool FilterFunc(Thing thing) => !thing.Map?.fogGrid.IsFogged(thing.Position) ?? true;
    }
}