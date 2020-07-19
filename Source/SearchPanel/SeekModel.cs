using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPanel
{
    public class SeekModel : ISeekModel
    {
        private readonly List<SearchItem> allItems = new List<SearchItem>();
        private readonly List<SearchItem> searchItems = new List<SearchItem>();
        private readonly List<ITextObserver> textObservers = new List<ITextObserver>();
        private readonly List<ICategoryObserver> categoryObservers = new List<ICategoryObserver>();
        private readonly List<ISearchItemObserver> searchItemObservers = new List<ISearchItemObserver>();

        private readonly SearchItemFactory searchItemFactory;

        private bool hasInitialized;
        private string text;
        private Category activeCategory;
        private SearchItem activeSearchItem;

        public List<Category> Categories { get; } = new List<Category>();
        public Category ActiveCategory
        {
            get => activeCategory;
            set
            {
                activeCategory = value;
                UpdateSearchItems();
                NotifyCategoryObservers();
            }
        }
        public IEnumerable<SearchItem> SearchItems { get => searchItems; }
        public SearchItem ActiveSearchItem
        {
            get => activeSearchItem;
            set
            {
                activeSearchItem = value;
                NotifySearchItemObservers();
            }
        }
        public string SearchText
        {
            get => text;
            set
            {
                text = value;
                UpdateSearchItems();
                NotifyTextObservers();
            }
        }
        public SeekModel(SearchItemFactory searchItemFactory)
        {
            this.searchItemFactory = searchItemFactory ?? throw new ArgumentNullException(nameof(searchItemFactory));
        }

        private void NotifyTextObservers() => textObservers.ForEach(to => to.AfterUpdateText());

        private void NotifyCategoryObservers() => categoryObservers.ForEach(co => co.AfterUpdateCategory());

        private void NotifySearchItemObservers() => searchItemObservers.ForEach(so => so.AfterUpdateSearchItem());

        public void AddFavourite(SearchItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveFavourite(SearchItem item)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            if (!hasInitialized)
            {
                Categories.AddRange(CategoryFactory.GetCategories());
                ActiveCategory = Categories.FirstOrDefault();
                SearchText = string.Empty;
                hasInitialized = true;
            }
        }

        public void RegisterObserver(ITextObserver textObserver)
        {
            textObservers.Add(textObserver);
        }

        public void RegisterObserver(ICategoryObserver categoryObserver)
        {
            categoryObservers.Add(categoryObserver);
        }

        public void RegisterObserver(ISearchItemObserver searchItemObserver)
        {
            searchItemObservers.Add(searchItemObserver);
        }

        public void RemoveObserver(ITextObserver textObserver)
        {
            textObservers.Remove(textObserver);
        }

        public void RemoveObserver(ICategoryObserver categoryObserver)
        {
            categoryObservers.Remove(categoryObserver);
        }

        public void RemoveObserver(ISearchItemObserver searchItemObserver)
        {
            searchItemObservers.Remove(searchItemObserver);
        }

        public void UpdateAllItems()
        {
            allItems.Clear();
            allItems.AddRange(searchItemFactory.GetSearchItems());
        }

        public void UpdateSearchItems()
        {
            searchItems.Clear();

            // TODO: Added for tests, del later.
            UpdateAllItems();

            IEnumerable<SearchItem> items = allItems;
            if (activeCategory != null)
            {
                items = activeCategory.GetFilteredItems(items);
            }
            if (!string.IsNullOrEmpty(text))
            {
                items = items.Where(i => i.Label.IndexOf(text, 0, StringComparison.OrdinalIgnoreCase) != -1);
            }
            searchItems.AddRange(items);
        }
    }
}
