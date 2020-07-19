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
        public override string Name { get; }

        public CategoryViaIgnoreRequestGroup(ThingRequestGroup group, Category child = null) : base(group, child)
        {
            Name = base.Name + " ignore";
        }

        protected override bool IsThingDefOfThisCategory(ThingDef def)
        {
            return !base.IsThingDefOfThisCategory(def);
        }
    }
}
