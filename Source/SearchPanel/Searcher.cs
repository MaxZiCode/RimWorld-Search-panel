using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace SearchPanel
{
    public abstract class Searcher<T>
    {
        private IEnumerable<T> filteredItems;

        protected readonly Dictionary<string, SearchItem<T>> nameBySearchItemsMap = new Dictionary<string, SearchItem<T>>(StringComparer.OrdinalIgnoreCase);

        public void UpdateItems(Map map, Filter<T> filter)
        {
            var items = GetItems(map);
            filteredItems = items.Where(i => filter.IsRight(i));
            ClearSearchItems();
            FindSearchItems();
            UpdateSearchItems();
        }

        public virtual IEnumerable<SearchItem<T>> GetSearchItems()
        {
            return nameBySearchItemsMap.Values.Where(si => si.Count != 0);
        }

        private void ClearSearchItems()
        {
            foreach (var searchItem in nameBySearchItemsMap.Values)
            {
                searchItem.Clear();
            }
        }

        private void FindSearchItems()
        {
            foreach (var item in filteredItems)
            {
                string key = GetKey(item);
                bool hasValue = nameBySearchItemsMap.TryGetValue(key, out SearchItem<T> searchItem);
                if (hasValue)
                {
                    searchItem.Add(item);
                }
                else
                {
                    searchItem = CreateSearchItem(item);
                    nameBySearchItemsMap.Add(key, searchItem);
                }
            }
        }

        private void UpdateSearchItems()
        {
            foreach (var searchItem in nameBySearchItemsMap.Values)
            {
                searchItem.Update();
            }
        }

        protected abstract IEnumerable<T> GetItems(Map map);

        protected abstract string GetKey(T item);

        protected abstract SearchItem<T> CreateSearchItem(T item);
    }
}