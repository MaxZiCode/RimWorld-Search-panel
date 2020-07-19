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
    public class RequestGroupCategory : Category
    {
        private readonly List<ThingRequestGroup> groups = new List<ThingRequestGroup>();
        private readonly List<ThingRequestGroup> ignorGroups = new List<ThingRequestGroup>();

        public RequestGroupCategory(string label)
        {
            this.Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        public static RequestGroupCategory Create(IEnumerable<ThingRequestGroup> groups, IEnumerable<ThingRequestGroup> ignoreGroups)
        {
            var category = new RequestGroupCategory(groups.First().ToString());
            foreach (var group in groups)
            {
                category.AddGroup(group);
            }
            foreach (var ignoreGroup in ignoreGroups)
            {
                category.AddIgnorGroup(ignoreGroup);
            }
            return category;
        }

        public void AddGroup(ThingRequestGroup group) => groups.Add(group);

        public void AddIgnorGroup(ThingRequestGroup group) => ignorGroups.Add(group);

        public override IEnumerable<SearchItem> GetFilteredItems(IEnumerable<SearchItem> searchItems)
        {
            foreach (var item in searchItems)
            {
                if (item.Def is ThingDef tDef &&
                    groups.All(g => ThingListGroupHelper.Includes(g, tDef)) &&
                    !ignorGroups.Any(ig => ThingListGroupHelper.Includes(ig, tDef)))
                {
                    yield return item;
                }
            }
        }
    }
}
