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
    public class SearchItemPack : Collection<SearchItem>
    {
        public string Label => Items.FirstOrDefault().LabelWithStuff ?? string.Empty;
        public int StackCount => Items.Sum(i => i.Count);
        public Texture2D Texture => Items.FirstOrDefault().Texture ?? BaseContent.BadTex;
        public Color Color => Items.FirstOrDefault().Color;
        public IEnumerable<IntVec3> AllCells => Items.SelectMany(i => i.Cells);

        public SearchItemPack(IEnumerable<SearchItem> searchItems)
        {
            foreach (var searchItem in searchItems)
            {
                Add(searchItem);
            }
        }

        public SearchItemPack(SearchItem searchItem)
        {
            Add(searchItem);
        }
    }
}