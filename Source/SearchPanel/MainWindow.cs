using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace SearchPanel
{
	public sealed class MainWindow : Window, ITextObserver, ICategoryObserver, ISearchItemObserver
	{
        private const float scrollSize = 16f;

		private Rect _positionRect;
		private Vector2 _initialSize = new Vector2(300f, 480f);
		private Vector2 _categoryScrollPosition = new Vector2();
		private Vector2 _itemsScrollPosition = new Vector2();
		private string _text;
		private Category _activeCategory;
		private SearchItem _activeSearchItem;

		private readonly ISeekModel _model;
		private readonly ISeekController _controller;

		public override Vector2 InitialSize => _initialSize;

		public MainWindow(ISeekController controller, ISeekModel model) : base()
		{
			this.doCloseX = false;
			this.preventDrawTutor = true;
			this.draggable = false;
			this.preventCameraMotion = false;
			this.closeOnAccept = false;
			this.focusWhenOpened = true;

			_positionRect = new Rect()
			{
				x = UI.screenWidth - InitialSize.x,
				y = UI.screenHeight - InitialSize.y - 150f,
				width = InitialSize.x,
				height = InitialSize.y
			};

			_controller = controller;
			_model = model;

			_model.RegisterObserver((ITextObserver)this);
			_model.RegisterObserver((ICategoryObserver)this);
			_model.RegisterObserver((ISearchItemObserver)this);


		}

		protected override void SetInitialSizeAndPosition() => windowRect = _positionRect;

		public override void DoWindowContents(Rect inRect)
		{
			Rect searchRect = new Rect(inRect)
			{
				height = 35f
			};
			DrawSearch(searchRect);

			Rect categoriesRect = new Rect(inRect)
			{
				y = searchRect.yMax,
				height = 35f + scrollSize
			};
			DrawCategories(categoriesRect);

			Rect resultsRect = new Rect(inRect)
			{
				yMin = categoriesRect.yMax,
			};
			DrawResults(resultsRect);
		}

		private void DrawSearch(Rect inRect)
		{
			_text = Widgets.TextField(inRect, _text);
			_controller.ChangeText(_text);
		}

		private void DrawCategories(Rect inRect)
		{
			const float gap = 0f;

			Rect faceRect = new Rect(inRect);

			float catRectSide = faceRect.height - scrollSize;
			var catRects = _model.Categories.Select((c, i) => (Filter: c, Rect: new Rect() { width = catRectSide, height = catRectSide, x = (catRectSide + gap) * i } ));

			Rect groupRect = new Rect()
			{
				width = catRects.LastOrDefault().Rect.xMax,
				height = catRectSide
			};

			Category selectedCategory = null;

			Widgets.BeginScrollView(faceRect, ref _categoryScrollPosition, groupRect);
			GUI.BeginGroup(groupRect);

			foreach (var catRect in catRects)
			{
				Rect curRect = catRect.Rect.ContractedBy(2f);
				Category category = catRect.Filter;
				Widgets.Label(curRect, category.Label.First().ToString());
				bool selected = category == _activeCategory;
				if (SimpleButton(curRect, selected))
				{
					selectedCategory = category;
				}
			}

            TurnVerticalScrollToHorizontal();

			GUI.EndGroup();
			Widgets.EndScrollView();

			_controller.ChangeActiveCategory(selectedCategory);
		}

        private void TurnVerticalScrollToHorizontal()
        {
            const float reduceFactor = 3f;
            var verticalScroll = Event.current.delta.y;
            Vector2 scrollVector = new Vector2(verticalScroll / reduceFactor, verticalScroll);
            Event.current.delta = scrollVector;
        }

        private void DrawResults(Rect inRect)
		{
            Rect faceRect = inRect.ContractedBy(1f);

            Rect drawMenuRect = new Rect(inRect)
            {
                width = inRect.width - scrollSize
            };
			Widgets.DrawMenuSection(drawMenuRect);

			Text.Font = GameFont.Small;

			var itemRects = _model.SearchItems.Select((si, i) => (Item: si, Rect: new Rect() { width = faceRect.width - scrollSize, height = Text.LineHeight, y = Text.LineHeight * i }));

			Rect resultRect = new Rect()
			{
				width = faceRect.width - scrollSize,
				height = itemRects.LastOrDefault().Rect.yMax
			};

			Widgets.BeginScrollView(faceRect, ref _itemsScrollPosition, resultRect);
			GUI.BeginGroup(resultRect);

			foreach (var itemRect in itemRects)
			{
				Rect curRect = itemRect.Rect;
                var item = itemRect.Item;

                Rect collapseButtonRect = curRect;
                collapseButtonRect.width = collapseButtonRect.height;

                Rect textureRect = collapseButtonRect;
                textureRect.x = collapseButtonRect.xMax;

                Rect favRect = textureRect;
                favRect.x = curRect.xMax - favRect.width;

                Rect countRect = favRect;
                countRect.width = Text.CalcSize(itemRect.Item.Count.ToString()).x;
                countRect.x = favRect.xMin - countRect.width;

                Rect labelRect = new Rect(curRect)
                {
                    xMin = textureRect.xMax,
                    xMax = countRect.xMin
                };

                DoCollapseRevealButton(collapseButtonRect);
                DoTexture(textureRect, item.Texture);
                DoLabel(labelRect, item.Label);
                DoCount(countRect, item.Count);
                DoFavouriteButton(favRect, item);
			}

			GUI.EndGroup();
			Widgets.EndScrollView();
		}

        private void DoFavouriteButton(Rect favRect, SearchItem item)
        {
            Widgets.DrawHighlightIfMouseover(favRect);
            // TODO: Добавлять / удалять из избранного.
            Widgets.ButtonImage(favRect, Textures.FavoutireButton, false);
        }

        private void DoCount(Rect countRect, int count)
        {
            Widgets.Label(countRect, count.ToString());
        }

        private void DoLabel(Rect labelRect, string label)
        {
            Widgets.Label(labelRect, label);
        }

        private void DoTexture(Rect textureRect, Texture2D texture)
        {
            Widgets.DrawTextureFitted(textureRect, texture, 1f);
        }

        private void DoCollapseRevealButton(Rect inRect)
        {
            Texture2D tex = true ? Textures.RevealTexture : Textures.CollapseTexture;
            if (Widgets.ButtonImage(inRect, tex, true))
            {
                // TODO: Скрывать / расскрывать.
            }
        }

        public void AfterUpdateText()
		{
			_text = _model.SearchText;
		}

		public void AfterUpdateSearchItem()
		{
			_activeSearchItem = _model.ActiveSearchItem;
		}

		public void AfterUpdateCategory()
		{
			_activeCategory = _model.ActiveCategory;
		}

		private bool SimpleButton(Rect rect, bool selected)
		{
			Widgets.DrawOptionBackground(rect, selected);
			return Widgets.ButtonInvisible(rect);
		}
	}
}
