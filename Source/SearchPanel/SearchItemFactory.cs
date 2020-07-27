using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace SearchPanel
{
    public abstract class SearchItemFactory
    {
        public IEnumerable<SearchItem> GetSearchItems(Map map) => GetMapItems(map).Concat(GetTerrains(map));

        public abstract IEnumerable<SearchItem> GetTerrains(Map map);

        public abstract IEnumerable<SearchItem> GetMapItems(Map map);
    }
}