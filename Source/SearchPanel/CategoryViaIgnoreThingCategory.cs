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
        public CategoryViaIgnoreThingCategory(ThingCategory thingCategory, Category child = null) : base(thingCategory, child)
        {
        }

        protected override bool IsSearchDefOfThisCategory(ThingDef def) => !base.IsSearchDefOfThisCategory(def);
    }
}
