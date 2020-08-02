using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public abstract class Filter<T>
    {
        private readonly Filter<T> child;

        protected Filter(Filter<T> child = null)
        {
            this.child = child;
        }

        public virtual bool IsRight(T item)
        {
            if (FilterFunc(item))
                return child?.FilterFunc(item) ?? true;
            return false;
        }

        protected abstract bool FilterFunc(T item);
    }
}