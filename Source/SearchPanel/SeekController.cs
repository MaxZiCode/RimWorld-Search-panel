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

        public void AddFavourite(ISearchable item)
        {
            model.AddFavourite(item);
        }

        public void ChangeActiveCategory(Category category)
        {
            if (ReferenceEquals(model.ActiveCategory, category))
            {
                model.ActiveCategory = model.Categories.FirstOrDefault();
            }
            else
            {
                model.ActiveCategory = category;
            }
        }

        public void ChangeActiveSearchable(ISearchable item)
        {
            if (model.ActiveSearchable.Equals(item))
            {
                model.ActiveSearchable = EmptySearchable.Get();
            }
            else
            {
                model.ActiveSearchable = item;
            }
        }

        public void ChangeText(string text)
        {
            if (text != model.SearchText)
                model.SearchText = text;
        }

        public void RemoveFavourite(ISearchable item)
        {
            model.RemoveFavourite(item);
        }
    }
}