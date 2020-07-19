using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class CategoryViaIgnoreThingCategory : CategoryViaThingCategory
    {
        public override string Name { get; }

        public CategoryViaIgnoreThingCategory(ThingCategory thingCategory, Category child = null) : base(thingCategory, child)
        {
            Name = base.Name + " ignore";
        }

        protected override bool IsSearchDefOfThisCategory(ThingDef def) => !base.IsSearchDefOfThisCategory(def);
    }
}
