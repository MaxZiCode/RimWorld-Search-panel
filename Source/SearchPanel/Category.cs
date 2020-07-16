using System.Collections.Generic;

namespace SearchPanel
{
	public abstract class Category
	{
		public string Label { get; set; }

		public abstract IEnumerable<SearchItem> GetFilteredItems(IEnumerable<SearchItem> searchItems);
	}
}