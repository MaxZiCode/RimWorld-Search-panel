using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class TerrainSearcher : Searcher<Terrain>
    {
        protected override SearchItem<Terrain> CreateSearchItem(Terrain item) => new TerrainSearchItem(item, GetKey(item));

        protected override IEnumerable<Terrain> GetItems(Map map) => map.terrainGrid.topGrid.Select((def, index) =>
            new Terrain() { Def = def, Place = CellIndicesUtility.IndexToCell(index, map.Size.x), Map = map });

        protected override string GetKey(Terrain item) => item.Def.LabelCap;
    }
}