using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.Noise;

namespace SearchPanel
{
	public interface ISeekModel
	{
        IReadOnlyCollection<Category> Categories { get; }
		IReadOnlyCollection<SearchItem> SearchItems { get; }
		Category ActiveCategory { get; set; }
		SearchItem ActiveSearchItem { get; set; }
		string SearchText { get; set; }

		void Initialize();

		void UpdateAllItems();
		void UpdateSearchItems();

		void AddFavourite(SearchItem item);
		void RemoveFavourite(SearchItem item);

		void RegisterObserver(ITextObserver textObserver);
		void RemoveObserver(ITextObserver textObserver);

		void RegisterObserver(ICategoryObserver categoryObserver);
		void RemoveObserver(ICategoryObserver categoryObserver);

		void RegisterObserver(ISearchItemObserver searchItemObserver);
		void RemoveObserver(ISearchItemObserver searchItemObserver);
	}
}
