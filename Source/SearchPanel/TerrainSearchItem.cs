using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public class TerrainSearchItem : SearchItem<TerrainDef>
    {
        private int count;

        public override int Count => count;
        public override Texture2D Texture { get; }
        public override Color Color { get; }
        public override BuildableDef Def { get; }
        public override ThingDef Stuff { get; }

        public TerrainSearchItem(TerrainDef def, string label) : base(def, label)
        {
            Def = def;
            Stuff = null;
            Texture = def.uiIcon;
            Color = def.uiIconColor;
        }

        public override IEnumerable<IntVec3> GetCells()
        {
            var map = Current.Game.CurrentMap;
            var terrainAndIndex = Current.Game.CurrentMap.terrainGrid.topGrid.Select((t, i) => (Terrain: t, Index: i));
            return terrainAndIndex.Where(ti => ti.Terrain == items.FirstOrDefault()).Select(ti => CellIndicesUtility.IndexToCell(ti.Index, map.Size.x));
        }

        public override void Update()
        {
            count = items.Count;
        }
    }
}