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
        IReadOnlyCollection<ISearchable> Searchables { get; }
        Category ActiveCategory { get; set; }
        ISearchable ActiveSearchable { get; set; }
        string SearchText { get; set; }

        void Initialize();

        void UpdateSearchItems();

        void AddFavourite(ISearchable item);

        void RemoveFavourite(ISearchable item);

        void RegisterObserver(ITextObserver textObserver);

        void RemoveObserver(ITextObserver textObserver);

        void RegisterObserver(ICategoryObserver categoryObserver);

        void RemoveObserver(ICategoryObserver categoryObserver);

        void RegisterObserver(ISearchableObserver searchItemObserver);

        void RemoveObserver(ISearchableObserver searchItemObserver);
    }
}