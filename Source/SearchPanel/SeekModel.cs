using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class SeekModel : ISeekModel
    {
        private readonly List<SearchItem> allItems = new List<SearchItem>();
        private readonly List<SearchItem> searchItems = new List<SearchItem>();
        private readonly List<Category> categories = new List<Category>();
        private readonly List<ITextObserver> textObservers = new List<ITextObserver>();
        private readonly List<ICategoryObserver> categoryObservers = new List<ICategoryObserver>();
        private readonly List<ISearchItemObserver> searchItemObservers = new List<ISearchItemObserver>();

        private readonly CategoryFactory categoryFactory;

        private bool hasInitialized;
        private string text;
        private Category activeCategory;
        private SearchItem activeSearchItem;

        public IReadOnlyCollection<Category> Categories => categories;

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

        public IReadOnlyCollection<SearchItem> SearchItems => searchItems;

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

        public SeekModel(CategoryFactory categoryFactory)
        {
            this.categoryFactory = categoryFactory;
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
                categories.AddRange(categoryFactory.GetCategories());
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
            var orderedItems = ActiveCategory.GetItems(Current.Game.CurrentMap).OrderBy(item => item.Label);
            allItems.AddRange(orderedItems);
        }

        public void UpdateSearchItems()
        {
            searchItems.Clear();

            // TODO: Added for tests, del later.
            UpdateAllItems();

            IEnumerable<SearchItem> items = allItems;
            if (activeCategory != null)
            {
                items = allItems;
            }
            if (!string.IsNullOrEmpty(text))
            {
                items = items.Where(i => i.Label.IndexOf(text, 0, StringComparison.OrdinalIgnoreCase) != -1);
            }
            searchItems.AddRange(items);
        }
    }
}