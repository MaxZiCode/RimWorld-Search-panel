using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class TerrainSearcher : Searcher<TerrainDef>
    {
        protected override SearchItem<TerrainDef> CreateSearchItem(TerrainDef item) => new TerrainSearchItem(item, GetKey(item));

        protected override IEnumerable<TerrainDef> GetItems(Map map) => map.terrainGrid.topGrid;

        protected override string GetKey(TerrainDef item) => item.LabelCap;
    }
}