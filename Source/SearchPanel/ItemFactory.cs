using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPanel
{
    public abstract class ItemFactory<T>
    {
        public abstract IEnumerable<T> GetItems();
    }
}