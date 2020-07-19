using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class CategoryViaIgnoreRequestGroup : CategoryViaRequestGroup
    {
        public CategoryViaIgnoreRequestGroup(ThingRequestGroup group, Category child = null) : base(group, child)
        {
        }

        protected override bool IsThingDefOfThisCategory(ThingDef def)
        {
            return !base.IsThingDefOfThisCategory(def);
        }
    }
}
