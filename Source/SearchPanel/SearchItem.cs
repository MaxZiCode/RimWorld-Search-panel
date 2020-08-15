using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public abstract class SearchItem<T> : ISearchable
    {
        protected readonly List<T> items = new List<T>();

        public virtual string Label { get; }
        public abstract int Count { get; }
        public abstract Texture2D Texture { get; }
        public abstract Color Color { get; }
        public abstract BuildableDef Def { get; }
        public abstract ThingDef Stuff { get; }

        protected SearchItem(T item, string label)
        {
            items.Add(item);
            Label = label;
        }

        public IReadOnlyCollection<T> Items => items;

        public void Clear() => items.Clear();

        public void Add(T item) => items.Add(item);

        public abstract IEnumerable<IntVec3> GetCells();

        public abstract void Update();
    }
}