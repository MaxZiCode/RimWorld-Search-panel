using UnityEngine;
using Verse;

namespace SearchPanel
{
	public interface ISearchItem
	{
		string Label { get; }

		Texture2D Texture { get; }

		int Count { get; }

		Def Def { get; }
	}
}