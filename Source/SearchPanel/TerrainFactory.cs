using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class TerrainFactory
    {
        public virtual IEnumerable<TerrainDef> GetTerrains(Map map) => map.terrainGrid.topGrid;
    }
}