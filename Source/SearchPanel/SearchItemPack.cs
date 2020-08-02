using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public struct SearchItemPack
    {
        private readonly List<SearchItem> searchItems;

        public string Label { get; set; }
        public int StackCount { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public IEnumerable<IntVec3> AllCells { get; set; }
        public IReadOnlyCollection<SearchItem> Items => searchItems;

        public SearchItemPack(IEnumerable<SearchItem> searchItems)
        {
            this.searchItems = new List<SearchItem>(searchItems);
            Label = searchItems.FirstOrDefault().LabelWithStuff ?? string.Empty;
            StackCount = searchItems.Sum(i => i.Count);
            Texture = searchItems.FirstOrDefault().Texture ?? BaseContent.BadTex;
            Color = searchItems.FirstOrDefault().Color;
            AllCells = searchItems.SelectMany(i => i.Cells);
        }
    }
}