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
        public readonly Category IgnoreCategory;
        public readonly Category Everything;
        public readonly Category CorpsesIgnore;
        public readonly Category FoodSources;
        public readonly Category Corpses;
        public readonly Category Pawns;
        public readonly Category Medicine;
        public readonly Category Weapons;
        public readonly Category Drugs;
        public readonly Category HarvestablePlants;
        public readonly Category Plants;
        public readonly Category Apparel;

        public CategoryFactory()
        {
            IgnoreCategory = new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Construction, // Blueprint + BuildingFrame
                             new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Filth));
            Everything = new CategoryViaRequestGroup(ThingRequestGroup.Everything, IgnoreCategory);
            CorpsesIgnore = new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Corpse);
            FoodSources = new CategoryViaRequestGroup(ThingRequestGroup.FoodSourceNotPlantOrTree, CorpsesIgnore);
            Corpses = new CategoryViaRequestGroup(ThingRequestGroup.Corpse);
            Pawns = new CategoryViaRequestGroup(ThingRequestGroup.Pawn);
            Medicine = new CategoryViaRequestGroup(ThingRequestGroup.Medicine);
            Weapons = new CategoryViaRequestGroup(ThingRequestGroup.Weapon);
            Drugs = new CategoryViaRequestGroup(ThingRequestGroup.Drug);
            HarvestablePlants = new CategoryViaRequestGroup(ThingRequestGroup.HarvestablePlant);
            Plants = new CategoryViaRequestGroup(ThingRequestGroup.Plant);
            Apparel = new CategoryViaRequestGroup(ThingRequestGroup.Apparel);
        }

        public virtual IEnumerable<Category> GetCategories()
        {
            yield return IgnoreCategory;
            yield return Everything;
            yield return CorpsesIgnore;
            yield return FoodSources;
            yield return Corpses;
            yield return Pawns;
            yield return Medicine;
            yield return Weapons;
            yield return Drugs;
            yield return HarvestablePlants;
            yield return Plants;
            yield return Apparel;

            foreach (ThingCategory tc in Enum.GetValues(typeof(ThingCategory)))
            {
                yield return new CategoryViaThingCategory(tc);
            }
        }
    }
}
