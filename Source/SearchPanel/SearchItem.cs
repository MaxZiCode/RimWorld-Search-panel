using RimWorld;
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

        public SearchItem(BuildableDef def, int count, ThingDef stuff = null)
        {
            Label = GenLabel.ThingLabel(def, stuff);
            Texture = def.uiIcon;
            Color = stuff != null ? def.GetColorForStuff(stuff) : def.uiIconColor;
            Count = count;
            Def = def;
        }
    }
}