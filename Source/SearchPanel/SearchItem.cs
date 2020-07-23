using UnityEngine;
using Verse;

namespace SearchPanel
{
    public struct SearchItem
    {
        public string Label { get; set; }

        public Texture2D Texture { get; set; }

        public int Count { get; set; }

        public BuildableDef Def { get; set; }

        public SearchItem(BuildableDef def, int count)
        {
            Label = def.LabelCap;
            Texture = def.uiIcon;
            Count = count;
            Def = def;
        }
    }
}