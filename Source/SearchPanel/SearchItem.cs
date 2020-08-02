using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public struct SearchItem
    {
        public string LabelCap { get; }
        public string LabelWithStuff { get; }
        public int Count { get; }
        public Texture2D Texture { get; }
        public Color Color { get; }
        public IReadOnlyCollection<IntVec3> Cells { get; }

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

        public SearchItem(TerrainDef def, IEnumerable<IntVec3> cells)
        {
            LabelCap = def.LabelCap;
            LabelWithStuff = GenLabel.ThingLabel(def, null);
            Texture = def.uiIcon;
            Color = def.uiIconColor;
            Cells = cells.ToList();
            Count = Cells.Count;
        }
    }
}