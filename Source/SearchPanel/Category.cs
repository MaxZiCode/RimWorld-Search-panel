using System.Collections.Generic;

namespace SearchPanel
{
	public abstract class Category
	{
        private class EmptyCategory : Category
        {
            public override string Name => "Empty";

            protected override IEnumerable<SearchItem> FilterItems(IEnumerable<SearchItem> searchItems) => searchItems;
        }

        public static readonly Category Empty = new EmptyCategory();

        protected readonly Category childCategory;

        public abstract string Name { get; }

        protected Category(Category child = null)
        {
            childCategory = child;
        }

        public IEnumerable<SearchItem> GetFilteredItems(IEnumerable<SearchItem> searchItems)
        {
            var childFilteredItems = childCategory?.GetFilteredItems(searchItems) ?? searchItems;
            return FilterItems(childFilteredItems);
        }

        protected abstract IEnumerable<SearchItem> FilterItems(IEnumerable<SearchItem> searchItems);
    }
}