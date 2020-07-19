using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class CategoryViaThingCategory : Category
    {
        protected ThingCategory thingCategory;

        public override string Name { get; }

        public CategoryViaThingCategory(ThingCategory thingCategory, Category child = null) : base(child)
        {
            this.thingCategory = thingCategory;
            Name = thingCategory.ToString();
        }

        protected override IEnumerable<SearchItem> FilterItems(IEnumerable<SearchItem> searchItems)
        {
            return searchItems.Where(si => si.Def is ThingDef tdef && IsSearchDefOfThisCategory(tdef));
        }

        protected virtual bool IsSearchDefOfThisCategory(ThingDef def) => def.category == thingCategory; 
    }
}
