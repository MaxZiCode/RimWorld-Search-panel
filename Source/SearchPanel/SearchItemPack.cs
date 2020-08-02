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
        public string Label { get; }
        public int StackCount { get; }
        public Texture2D Texture { get; }
        public Color Color { get; }
        public IEnumerable<IntVec3> AllCells { get; }
        public IReadOnlyCollection<SearchItem> Items { get; }

        public SearchItemPack(IEnumerable<SearchItem> searchItems)
        {
            Items = new List<SearchItem>(searchItems);
            Label = Items.FirstOrDefault().LabelWithStuff ?? string.Empty;
            StackCount = Items.Sum(i => i.Count);
            Texture = Items.FirstOrDefault().Texture ?? BaseContent.BadTex;
            Color = Items.FirstOrDefault().Color;
            AllCells = Items.SelectMany(i => i.Cells);
        }

        public SearchItemPack(SearchItem searchItem)
        {
            Items = new[] { searchItem };
            Label = searchItem.LabelWithStuff ?? string.Empty;
            StackCount = searchItem.Count;
            Texture = searchItem.Texture ?? BaseContent.BadTex;
            Color = searchItem.Color;
            AllCells = searchItem.Cells;
        }
    }
}