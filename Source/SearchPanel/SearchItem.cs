using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public struct SearchItem
    {
        public string LabelCap { get; set; }
        public string LabelWithStuff { get; set; }
        public int Count { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public IReadOnlyCollection<IntVec3> Cells { get; set; }

        public SearchItem(Thing thing)
        {
            var def = thing.def;
            var stuff = thing.Stuff;

            LabelCap = thing.LabelCap;
            LabelWithStuff = GenLabel.ThingLabel(def, stuff);
            Count = thing.stackCount;
            Texture = def.uiIcon;
            Color = stuff != null ? def.GetColorForStuff(stuff) : def.uiIconColor;
            Cells = thing.OccupiedRect().ToList();
        }

        public SearchItem(TerrainDef def, IntVec3 cell)
        {
            LabelCap = def.LabelCap;
            LabelWithStuff = GenLabel.ThingLabel(def, null);
            Count = 1;
            Texture = def.uiIcon;
            Color = def.uiIconColor;
            Cells = new[] { cell };
        }
    }
}