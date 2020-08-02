namespace SearchPanel
{
    public interface ISeekController
    {
        void ChangeText(string text);

        void ChangeActiveCategory(Category category);

        void ChangeActiveSearchItem(SearchItemPack searchItem);

        void AddFavourite(SearchItemPack item);

        void RemoveFavourite(SearchItemPack item);
    }
}