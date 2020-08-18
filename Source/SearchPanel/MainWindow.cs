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
    public sealed class MainWindow : Window, ITextObserver, ICategoryObserver, ISearchableObserver
    {
        private const float scrollSize = 16f;

        private Vector2 initialSize = new Vector2(300f, 480f);
        private Vector2 categoryScrollPosition = new Vector2();
        private Vector2 itemsScrollPosition = new Vector2();
        private string text;
        private Category activeCategory;
        private ISearchable activeSearchable;

        private readonly ISeekModel model;
        private readonly ISeekController controller;

        public override Vector2 InitialSize => initialSize;

        public MainWindow(ISeekController controller, ISeekModel model) : base()
        {
            doCloseX = false;
            preventDrawTutor = true;
            draggable = false;
            preventCameraMotion = false;
            closeOnAccept = false;
            focusWhenOpened = true;

            this.controller = controller;
            this.model = model;
            this.model.RegisterObserver((ITextObserver)this);
            this.model.RegisterObserver((ICategoryObserver)this);
            this.model.RegisterObserver((ISearchableObserver)this);
        }

        protected override void SetInitialSizeAndPosition() => windowRect = new Rect()
        {
            x = UI.screenWidth - InitialSize.x,
            y = UI.screenHeight - InitialSize.y - 150f,
            width = InitialSize.x,
            height = InitialSize.y
        };

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
            Text.Font = GameFont.Medium;
            text = Widgets.TextField(inRect, text);
            controller.ChangeText(text);
        }

        private void DrawCategories(Rect inRect)
        {
            Text.Font = GameFont.Small;

            Rect faceRect = new Rect(inRect);

            float catRectSide = faceRect.height - scrollSize;
            Rect categoryRect = new Rect() { width = catRectSide, height = catRectSide };

            Rect groupRect = new Rect()
            {
                width = categoryRect.width * model.Categories.Count,
                height = catRectSide
            };

            Widgets.BeginScrollView(faceRect, ref categoryScrollPosition, groupRect);
            GUI.BeginGroup(groupRect);

            foreach (var category in model.Categories)
            {
                Rect curRect = categoryRect.ContractedBy(2f);
                bool selected = category == activeCategory;
                if (SimpleButton(curRect, selected))
                {
                    controller.ChangeActiveCategory(category);
                }

                categoryRect.x += categoryRect.width;
            }

            TurnVerticalScrollToHorizontal();

            GUI.EndGroup();
            Widgets.EndScrollView();
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
            Text.Font = GameFont.Small;

            Rect faceRect = inRect.ContractedBy(1f);
            Rect drawMenuRect = new Rect(inRect)
            {
                width = inRect.width - scrollSize
            };
            Widgets.DrawMenuSection(drawMenuRect);

            Rect itemRect = new Rect() { width = faceRect.width - scrollSize, height = Text.LineHeight };
            Rect groupRect = new Rect()
            {
                width = faceRect.width - scrollSize,
                height = itemRect.height * model.Searchables.Count
            };

            Widgets.BeginScrollView(faceRect, ref itemsScrollPosition, groupRect);
            GUI.BeginGroup(groupRect);

            foreach (var searchable in model.Searchables)
            {
                Rect collapseButtonRect = itemRect;
                collapseButtonRect.width = collapseButtonRect.height;

                Rect textureRect = collapseButtonRect;
                textureRect.x = collapseButtonRect.xMax;

                Rect favRect = textureRect;
                favRect.x = itemRect.xMax - favRect.width;

                Rect countRect = favRect;
                countRect.width = Text.CalcSize(searchable.Count.ToString()).x;
                countRect.x = favRect.xMin - countRect.width;

                Rect labelRect = new Rect(itemRect)
                {
                    xMin = textureRect.xMax,
                    xMax = countRect.xMin
                };

                Rect buttonRect = new Rect(itemRect)
                {
                    xMin = textureRect.xMin,
                    xMax = countRect.xMax
                };

                bool selected = activeSearchable?.Equals(searchable) ?? false;
                if (selected)
                    Widgets.DrawOptionSelected(itemRect);

                DoSearchButton(buttonRect, searchable);
                DoCollapseRevealButton(collapseButtonRect);
                DoTexture(textureRect, searchable);
                DoLabel(labelRect, searchable);
                DoCount(countRect, searchable);
                DoFavouriteButton(favRect, searchable);

                itemRect.y += itemRect.height;
            }

            GUI.EndGroup();
            Widgets.EndScrollView();
        }

        private void DoFavouriteButton(Rect favRect, ISearchable item)
        {
            Widgets.DrawHighlightIfMouseover(favRect);
            // TODO: Добавлять / удалять из избранного.
            Widgets.ButtonImage(favRect, Textures.FavoutireButton, false);
        }

        private void DoCount(Rect countRect, ISearchable item)
        {
            Widgets.Label(countRect, item.Count.ToString());
        }

        private void DoLabel(Rect labelRect, ISearchable item)
        {
            Widgets.Label(labelRect, item.Label);
        }

        private void DoSearchButton(Rect buttonRect, ISearchable item)
        {
            Widgets.DrawHighlightIfMouseover(buttonRect);
            TooltipHandler.TipRegion(buttonRect, item.Label);
            if (Widgets.ButtonInvisible(buttonRect))
            {
                controller.ChangeActiveSearchable(item);
            }
        }

        private void DoTexture(Rect textureRect, ISearchable item)
        {
            GUI.color = item.Color;
            Widgets.DrawTextureFitted(textureRect, item.Texture, 1f);
            GUI.color = Color.white;
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
            text = model.SearchText;
        }

        public void AfterUpdateSearchItem()
        {
            activeSearchable = model.ActiveSearchable;
        }

        public void AfterUpdateCategory()
        {
            activeCategory = model.ActiveCategory;
        }

        private bool SimpleButton(Rect rect, bool selected)
        {
            Widgets.DrawOptionBackground(rect, selected);
            return Widgets.ButtonInvisible(rect);
        }
    }
}