using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Verse;
using Verse.Noise;

namespace SearchPanel
{
    public class CategoryViaRequestGroup : Category
    {
        protected ThingRequestGroup thingRequestGroup;

        public override string Name { get; }

        public CategoryViaRequestGroup(ThingRequestGroup group, Category child = null) : base(child)
        {
            thingRequestGroup = group;
            Name = group.ToString() + " RG";
        }

        protected override IEnumerable<SearchItem> FilterItems(IEnumerable<SearchItem> searchItems)
        {
            return searchItems.Where(item => item.Def is ThingDef tdef && IsThingDefOfThisCategory(tdef));
        }

        protected virtual bool IsThingDefOfThisCategory(ThingDef def) => ThingListGroupHelper.Includes(thingRequestGroup, def);
    }
}
