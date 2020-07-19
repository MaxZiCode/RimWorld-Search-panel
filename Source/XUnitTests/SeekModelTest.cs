using System;
using Xunit;
using SearchPanel;
using System.Collections.Generic;
using System.Linq;

namespace XUnitTests
{
	public class SeekModelTest
	{
        SeekModel model;
        Category category = Category.Empty;
        UltimateObserver observer = new UltimateObserver();

		[Fact]
		public void ActiveCategoryNotNull()
		{
            SetSeekModel();
            Assert.NotNull(model.ActiveCategory);
		}

        [Fact]
        public void SearchTextNotNull()
        {
            SetSeekModel();
            Assert.NotNull(model.SearchText);
        }

        [Fact]
        public void CategoryObserverTest()
        {
            SetSeekModel();
            observer.Clear();
            model.RegisterObserver((ICategoryObserver)observer);

            CheckObserver(uo => uo.HasCategoryUpdated, m => m.ActiveCategory = category);
        }

        [Fact]
        public void TextObserverTest()
        {
            SetSeekModel();
            observer.Clear();
            model.RegisterObserver((ITextObserver)observer);

            CheckObserver(uo => uo.HasTextUpdated, m => m.SearchText = string.Empty);
        }

        [Fact]
        public void SearchItemObserverTest()
        {
            SetSeekModel();
            observer.Clear();
            model.RegisterObserver((ISearchItemObserver)observer);

            CheckObserver(uo => uo.HasSearchItemUpdated, m => m.ActiveSearchItem = new SearchItem());
        }

        void SetSeekModel()
        {
            model = new SeekModel(new EmptySearchItemFactory(), new CategoryFactory());
            model.Initialize();
        }

        void CheckObserver(Func<UltimateObserver, bool> hasUpdatedFunc, Action<SeekModel> changeModelAction)
        {
            changeModelAction(model);
            Assert.True(hasUpdatedFunc(observer));

            changeModelAction(model);
            observer.Clear();
            Assert.False(hasUpdatedFunc(observer));
        }
    }

    public class EmptySearchItemFactory : SearchItemFactory
    {
        public override IEnumerable<SearchItem> GetSearchItems()
        {
            return Enumerable.Repeat(0, 5).Select(r => new SearchItem
            {
                Label = "Label",
            });
        }
    }

    public class UltimateObserver : ICategoryObserver, ITextObserver, ISearchItemObserver
    {
        public bool HasCategoryUpdated { get; set; }
        public bool HasSearchItemUpdated { get; set; }
        public bool HasTextUpdated { get; set; }

        public void AfterUpdateCategory()
        {
            HasCategoryUpdated = true;
        }

        public void AfterUpdateSearchItem()
        {
            HasSearchItemUpdated = true;
        }

        public void AfterUpdateText()
        {
            HasTextUpdated = true;
        }

        public void Clear()
        {
            HasCategoryUpdated = false;
            HasSearchItemUpdated = false;
            HasTextUpdated = false;
        }
    }
}
