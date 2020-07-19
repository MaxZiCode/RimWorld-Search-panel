using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
	static class CategoryFactory
	{
        private static readonly IEnumerable<ThingRequestGroup> empty = Enumerable.Empty<ThingRequestGroup>();

        private static readonly ThingRequestGroup[] alwaysSkipGroups = new[]
        {
            ThingRequestGroup.Filth,
            ThingRequestGroup.Construction // Blueprint + BuildingFrame
        };

		public static IEnumerable<Category> GetCategories()
		{
            yield return RequestGroupCategory.Create(new[] 
            { ThingRequestGroup.Everything }, alwaysSkipGroups);

            yield return RequestGroupCategory.Create(new[] 
            { ThingRequestGroup.FoodSourceNotPlantOrTree }, new[] { ThingRequestGroup.Corpse });

            yield return RequestGroupCategory.Create(new[] 
            { ThingRequestGroup.Corpse }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.BuildingArtificial }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.Pawn }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.Medicine }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.Weapon }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.Drug }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.HarvestablePlant }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.Plant }, empty);

            yield return RequestGroupCategory.Create(new[]
            { ThingRequestGroup.Apparel }, empty);
        }
	}
}
