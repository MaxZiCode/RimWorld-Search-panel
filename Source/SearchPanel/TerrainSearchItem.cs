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
    public class TerrainSearchItem : SearchItem<Terrain>
    {
        private int count;

        public override int Count => count;
        public override Texture2D Texture { get; }
        public override Color Color { get; }
        public override BuildableDef Def { get; }
        public override ThingDef Stuff { get; }

        public TerrainSearchItem(Terrain terrain, string label) : base(terrain, label)
        {
            Def = terrain.Def;
            Stuff = null;
            Texture = Def.uiIcon;
            Color = Def.uiIconColor;
            items.Add(terrain);
        }

        public override IEnumerable<IntVec3> GetCells() => items.Select(i => i.Place);

        public override void Update()
        {
            count = items.Count;
        }
    }
}