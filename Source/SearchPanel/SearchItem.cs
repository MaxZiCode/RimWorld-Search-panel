using UnityEngine;
using Verse;

namespace SearchPanel
{
    public struct SearchItem
    {
        public string Label { get; set; }

        public Texture2D Texture { get; set; }

        public int Count { get; set; }

        public Def Def { get; set; }
    }
}