namespace SearchPanel
{
	public interface ISeekController
	{
		void ChangeText(string text);
		void ChangeActiveCategory(Category category);
		void ChangeActiveSearchItem(SearchItem searchItem);
		void AddFavourite(SearchItem item);
		void RemoveFavourite(SearchItem item);
	}
}