using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public struct SearchItem
    {
        public string Label { get; set; }

        public Texture2D Texture { get; set; }

        public Color Color { get; set; }

        public int Count { get; set; }

        public BuildableDef Def { get; set; }

        public List<IntVec3> Locations { get; set; }

        public SearchItem(BuildableDef def, IEnumerable<IntVec3> locations, int count, ThingDef stuff = null)
        {
            Locations = new List<IntVec3>(locations);
            Label = GenLabel.ThingLabel(def, stuff);
            Texture = def.uiIcon;
            Color = stuff != null ? def.GetColorForStuff(stuff) : def.uiIconColor;
            Count = count;
            Def = def;
        }
    }
}