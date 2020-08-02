using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class SeekController : ISeekController
    {
        private readonly ISeekModel model;

        public SeekController(ISeekModel model)
        {
            this.model = model;
        }

        public void AddFavourite(SearchItemPack item)
        {
            model.AddFavourite(item);
        }

        public void ChangeActiveCategory(Category category)
        {
            if (category == null)
                return;

            if (model.ActiveCategory == category)
            {
                model.ActiveCategory = null;
            }
            else
            {
                model.ActiveCategory = category;
            }
        }

        public void ChangeActiveSearchItem(SearchItemPack item)
        {
            model.ActiveSearchItemPack = item;
        }

        public void ChangeText(string text)
        {
            if (text != model.SearchText)
                model.SearchText = text;
        }

        public void RemoveFavourite(SearchItemPack item)
        {
            model.RemoveFavourite(item);
        }
    }
}