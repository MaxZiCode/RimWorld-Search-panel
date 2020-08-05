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
        private readonly List<SearchItemPack> searchItems = new List<SearchItemPack>();
        private readonly List<Category> categories = new List<Category>();
        private readonly List<ITextObserver> textObservers = new List<ITextObserver>();
        private readonly List<ICategoryObserver> categoryObservers = new List<ICategoryObserver>();
        private readonly List<ISearchItemObserver> searchItemObservers = new List<ISearchItemObserver>();

        private readonly CategoryFactory categoryFactory;

        private bool hasInitialized;
        private string text;
        private Map map;
        private Category activeCategory;
        private SearchItemPack activeSearchItemPack;

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

        public IReadOnlyCollection<SearchItemPack> SearchItems => searchItems;

        public SearchItemPack ActiveSearchItemPack
        {
            get => activeSearchItemPack;
            set
            {
                activeSearchItemPack = value;
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

        public void AddFavourite(SearchItemPack item)
        {
            throw new NotImplementedException();
        }

        public void RemoveFavourite(SearchItemPack item)
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

        public void UpdateSearchItems()
        {
            var currentMap = Current.Game.CurrentMap;

            IEnumerable<SearchItemPack> items = ActiveCategory.GetItems(currentMap).OrderBy(item => item.Label);
            if (!string.IsNullOrEmpty(text))
            {
                items = items.Where(i => i.Label.IndexOf(text, 0, StringComparison.OrdinalIgnoreCase) != -1);
            }
            searchItems.Clear();
            searchItems.AddRange(items);

            if (map != currentMap)
            {
                map = currentMap;
                ActiveSearchItemPack = items.FirstOrDefault(i => ActiveSearchItemPack.Equals(i));
            }
        }
    }
}