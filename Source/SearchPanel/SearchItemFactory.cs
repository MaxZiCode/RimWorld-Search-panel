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
        public abstract IEnumerable<SearchItem> GetSearchItems();
    }
}
