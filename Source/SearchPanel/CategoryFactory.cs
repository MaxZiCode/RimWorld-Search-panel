using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class CategoryFactory
    {
        public readonly Category Ignore;
        public readonly Category Everything;
        public readonly Category CorpsesIgnore;

        public CategoryFactory()
        {
            Ignore = new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Construction, // Blueprint + BuildingFrame
                             new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Filth,
                             new CategoryViaIgnoreThingCategory(ThingCategory.Mote,
                             new CategoryViaIgnoreThingCategory(ThingCategory.Ethereal))));
            Everything = new CategoryViaRequestGroup(ThingRequestGroup.Everything, Ignore);
            CorpsesIgnore = new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Corpse, Everything);
        }

        public virtual IEnumerable<Category> GetCategories()
        {
            // TODO: Favourites
            yield return Everything;
            yield return new CategoryViaThingCategory(ThingCategory.Building, Everything);
            // TODO: Terrains
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Plant, Everything);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.HarvestablePlant, Everything);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.FoodSourceNotPlantOrTree, CorpsesIgnore);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Pawn, Everything);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Corpse, Everything);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Apparel, Everything);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Weapon, Everything);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Medicine, Everything);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Drug, Everything);
        }
    }
}
