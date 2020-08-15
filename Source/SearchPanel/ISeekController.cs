namespace SearchPanel
{
    public interface ISeekController
    {
        void ChangeText(string text);

        void ChangeActiveCategory(Category category);

        void ChangeActiveSearchable(ISearchable searchItem);

        void AddFavourite(ISearchable item);

        void RemoveFavourite(ISearchable item);
    }
}