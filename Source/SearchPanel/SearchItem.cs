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
        public BuildableDef Def { get; }
        public ThingDef Stuff { get; }
        public IReadOnlyCollection<IntVec3> Cells { get; }

        public SearchItem(Thing thing)
        {
            var innerThing = thing is MinifiedThing mThing ? mThing.InnerThing : thing;

            Stuff = innerThing.Stuff;
            Def = innerThing.def;
            LabelCap = innerThing.LabelCap;
            LabelWithStuff = GenLabel.ThingLabel(Def, Stuff);
            Count = thing.stackCount;
            Texture = Def.uiIcon;
            Color = Stuff != null ? Def.GetColorForStuff(Stuff) : Def.uiIconColor;
            Cells = thing.OccupiedRect().ToList();
        }

        public SearchItem(TerrainDef def, IEnumerable<IntVec3> cells)
        {
            Def = def;
            Stuff = null;
            LabelCap = def.LabelCap;
            LabelWithStuff = GenLabel.ThingLabel(def, null);
            Texture = def.uiIcon;
            Color = def.uiIconColor;
            Cells = cells.ToList();
            Count = Cells.Count;
        }
    }
}