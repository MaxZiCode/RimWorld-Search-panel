using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public struct Terrain
    {
        public TerrainDef Def { get; set; }

        public IntVec3 Place { get; set; }

        public Map Map { get; set; }
    }
}