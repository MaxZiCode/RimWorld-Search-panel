using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class TerrainCategory : Category
    {
        public override string Name { get; } = "Terrain";

        protected override IEnumerable<SearchItem> FilterItems(IEnumerable<SearchItem> searchItems) => searchItems.Where(si => si.Def is TerrainDef);
    }
}
